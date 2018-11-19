using System;
using System.IO;
using System.Reactive.Linq;
using IO.Proximax.SDK.Connections;
using IO.Proximax.SDK.Exceptions;
using IO.Proximax.SDK.Models;
using IO.Proximax.SDK.Services.Factories;
using IO.Proximax.SDK.Upload;
using IO.Proximax.SDK.Utils;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

namespace IO.Proximax.SDK.Services
{
    public class CreateProximaxDataService
    {
        private FileUploadService FileUploadService { get; }
        private ContentTypeUtils ContentTypeUtils { get; }

        public CreateProximaxDataService(IFileStorageConnection fileStorageConnection)
        {
            FileUploadService = new FileUploadService(FileRepositoryFactory.Create(fileStorageConnection));
            ContentTypeUtils = new ContentTypeUtils();
        }

        internal CreateProximaxDataService(FileUploadService fileUploadService, ContentTypeUtils contentTypeUtils)
        {
            FileUploadService = fileUploadService;
            ContentTypeUtils = contentTypeUtils;
        }

        public IObservable<ProximaxDataModel> CreateData(UploadParameter uploadParam)
        {
            CheckParameter(uploadParam != null, "uploadParam is required");

            switch (uploadParam.Data)
            {
                case IByteStreamParameterData _: // when byte stream upload
                    return UploadByteStream(uploadParam, (IByteStreamParameterData) uploadParam.Data);
                case PathParameterData _: // when path upload
                    return UploadPath((PathParameterData) uploadParam.Data);
                default: // when unknown data
                    throw new UploadParameterDataNotSupportedException(
                        $"Uploading of {uploadParam.Data.GetType()} is not supported");
            }
        }

        private IObservable<ProximaxDataModel> UploadByteStream(UploadParameter uploadParam,
            IByteStreamParameterData byteStreamParamData)
        {
            var detectedContentTypeOb = DetectContentType(uploadParam, byteStreamParamData);
            var fileUploadResponseOb = FileUploadService.UploadByteStream(byteStreamParamData.GetByteStream,
                uploadParam.PrivacyStrategy, uploadParam.ComputeDigest);

            return Observable.Zip(fileUploadResponseOb, detectedContentTypeOb,
                (fileUploadResponse, contentTypeOpt) =>
                    ProximaxDataModel.Create(byteStreamParamData, fileUploadResponse.DataHash,
                        fileUploadResponse.Digest, contentTypeOpt, fileUploadResponse.Timestamp));
        }

        private IObservable<string> DetectContentType(UploadParameter uploadParam,
            IByteStreamParameterData byteStreamParamData)
        {
            return uploadParam.DetectContentType && byteStreamParamData.ContentType == null
                ? ContentTypeUtils.DetectContentType(byteStreamParamData.GetByteStream())
                : Observable.Return(byteStreamParamData.ContentType);
        }

        private Stream EncryptByteStream(UploadParameter uploadParam, IByteStreamParameterData byteStreamParamData)
        {
            return uploadParam.PrivacyStrategy.EncryptStream(byteStreamParamData.GetByteStream());
        }

        private IObservable<ProximaxDataModel> UploadPath(PathParameterData pathParamData)
        {
            return FileUploadService.UploadPath(pathParamData.Path).Select(ipfsUploadResponse =>
                ProximaxDataModel.Create(pathParamData, ipfsUploadResponse.DataHash,
                    null, pathParamData.ContentType, ipfsUploadResponse.Timestamp));
        }
    }
}