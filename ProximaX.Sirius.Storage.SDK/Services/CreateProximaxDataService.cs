using System;
using System.IO;
using System.Reactive.Linq;
using ProximaX.Sirius.Storage.SDK.Connections;
using ProximaX.Sirius.Storage.SDK.Exceptions;
using ProximaX.Sirius.Storage.SDK.Models;
using ProximaX.Sirius.Storage.SDK.Services.Factories;
using ProximaX.Sirius.Storage.SDK.Upload;
using ProximaX.Sirius.Storage.SDK.Utils;
using static ProximaX.Sirius.Storage.SDK.Utils.ParameterValidationUtils;

namespace ProximaX.Sirius.Storage.SDK.Services
{
    public class CreateProximaxDataService
    {
        private FileUploadService FileUploadService { get; }

        public CreateProximaxDataService(IFileStorageConnection fileStorageConnection)
        {
            FileUploadService = new FileUploadService(FileRepositoryFactory.Create(fileStorageConnection));
        }

        internal CreateProximaxDataService(FileUploadService fileUploadService)
        {
            FileUploadService = fileUploadService;
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
            var detectedContentType = DetectContentType(uploadParam, byteStreamParamData);
            var fileUploadResponseOb = FileUploadService.UploadByteStream(byteStreamParamData.GetByteStream,
                uploadParam.PrivacyStrategy, uploadParam.ComputeDigest);

            return fileUploadResponseOb.Select(fileUploadResponse =>
                ProximaxDataModel.Create(byteStreamParamData, fileUploadResponse.DataHash,
                    fileUploadResponse.Digest, detectedContentType, fileUploadResponse.Timestamp));
        }

        private string DetectContentType(UploadParameter uploadParam,
            IByteStreamParameterData byteStreamParamData)
        {
            return uploadParam.DetectContentType && byteStreamParamData.ContentType == null
                ? byteStreamParamData.GetByteStream().DetectContentType()
                : byteStreamParamData.ContentType;
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