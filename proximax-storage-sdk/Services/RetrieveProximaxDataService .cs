using System;
using System.IO;
using Proximax.Storage.SDK.Connections;
using Proximax.Storage.SDK.PrivacyStrategies;
using static Proximax.Storage.SDK.Models.Constants;
using static Proximax.Storage.SDK.Utils.ParameterValidationUtils;

namespace Proximax.Storage.SDK.Services
{
    public class RetrieveProximaxDataService
    {
        private FileDownloadService FileDownloadService { get; }

        public RetrieveProximaxDataService(IFileStorageConnection fileStorageConnection)
        {
            FileDownloadService = new FileDownloadService(fileStorageConnection);
        }

        internal RetrieveProximaxDataService(FileDownloadService fileDownloadService)
        {
            FileDownloadService = fileDownloadService;
        }

        public IObservable<Stream> GetDataByteStream(string dataHash, IPrivacyStrategy privacyStrategy,
            bool validateDigest,
            string digest, string contentType)
        {
            CheckParameter(dataHash != null, "dataHash is required");
            CheckParameter(privacyStrategy != null, "privacyStrategy is required");

            if (contentType != null && contentType.Equals(PathUploadContentType))
            {
                // path
                throw new NotSupportedException("download of path is not yet supported");
            }
            else
            {
                // byte array
                var digestToUse = validateDigest ? digest : null;
                return FileDownloadService.GetByteStream(dataHash, privacyStrategy, digestToUse);
            }
        }
    }
}