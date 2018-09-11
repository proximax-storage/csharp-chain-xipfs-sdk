using System;
using System.IO;
using System.Reactive.Linq;
using IO.Proximax.SDK.Connections;
using IO.Proximax.SDK.Exceptions;
using IO.Proximax.SDK.Models;
using IO.Proximax.SDK.Upload;
using IO.Proximax.SDK.Utils;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

namespace IO.Proximax.SDK.Services
{
    public class CreateProximaxDataService
    {
        private IpfsUploadService IpfsUploadService { get; }
        private DigestUtils DigestUtils { get; }
        private ContentTypeUtils ContentTypeUtils { get; }

        public CreateProximaxDataService(IpfsConnection ipfsConnection)
        {
            IpfsUploadService = new IpfsUploadService(ipfsConnection);
            DigestUtils = new DigestUtils();
            ContentTypeUtils = new ContentTypeUtils();
        }

        internal CreateProximaxDataService(IpfsUploadService ipfsUploadService, DigestUtils digestUtils, ContentTypeUtils contentTypeUtils)
        {
            IpfsUploadService = ipfsUploadService;
            DigestUtils = digestUtils;
            ContentTypeUtils = contentTypeUtils;
        }

        public IObservable<ProximaxDataModel> CreateData(UploadParameter uploadParam) {
            CheckParameter(uploadParam != null, "uploadParam is required");
    
            switch (uploadParam.Data)
            {
                case IByteStreamParameterData _: // when byte stream upload
                    return UploadByteStream(uploadParam, (IByteStreamParameterData) uploadParam.Data);
                case PathParameterData _: // when path upload
                    return UploadPath((PathParameterData) uploadParam.Data);
                default: // when unknown data
                    throw new UploadParameterDataNotSupportedException($"Uploading of {uploadParam.Data.GetType()} is not supported");
            }
        }
    
        private IObservable<ProximaxDataModel> UploadByteStream(UploadParameter uploadParam, IByteStreamParameterData byteStreamParamData) {
            var detectedContentTypeOb = DetectContentType(uploadParam, byteStreamParamData);
            var encryptedByteStream = EncryptByteStream(uploadParam, byteStreamParamData);
            var digestOb = ComputeDigest(uploadParam.ComputeDigest, EncryptByteStream(uploadParam, byteStreamParamData));
            var ipfsUploadResponseOb = IpfsUploadService.UploadByteStream(encryptedByteStream);
    
            return Observable.Zip(ipfsUploadResponseOb, digestOb, detectedContentTypeOb,
                    (ipfsUploadResponse, digest, contentTypeOpt) =>
                            ProximaxDataModel.Create(byteStreamParamData, ipfsUploadResponse.DataHash,
                                    digest, contentTypeOpt, ipfsUploadResponse.Timestamp));
        }
    
        private IObservable<string> DetectContentType(UploadParameter uploadParam, IByteStreamParameterData byteStreamParamData) {
            return uploadParam.DetectContentType&& byteStreamParamData.ContentType == null
                    ? ContentTypeUtils.DetectContentType(byteStreamParamData.GetByteStream())
                    : Observable.Return(byteStreamParamData.ContentType);
        }
    
        private Stream EncryptByteStream(UploadParameter uploadParam, IByteStreamParameterData byteStreamParamData) {
            return uploadParam.PrivacyStrategy.EncryptStream(byteStreamParamData.GetByteStream());
        }
    
        private IObservable<string> ComputeDigest(bool computeDigest, Stream encryptedStream) {
            return computeDigest ? DigestUtils.Digest(encryptedStream) : Observable.Return<string>(null);
        }
    
        private IObservable<ProximaxDataModel> UploadPath(PathParameterData pathParamData) {
            return IpfsUploadService.UploadPath(pathParamData.Path).Select(ipfsUploadResponse =>
                    ProximaxDataModel.Create(pathParamData, ipfsUploadResponse.DataHash,
                            null,  pathParamData.ContentType, ipfsUploadResponse.Timestamp));
        }
    }
}
