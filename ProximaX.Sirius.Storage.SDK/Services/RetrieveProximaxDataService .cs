using System;
using System.IO;
using ProximaX.Sirius.Storage.SDK.Connections;
using ProximaX.Sirius.Storage.SDK.PrivacyStrategies;
using static ProximaX.Sirius.Storage.SDK.Models.Constants;
using static ProximaX.Sirius.Storage.SDK.Utils.ParameterValidationUtils;

namespace ProximaX.Sirius.Storage.SDK.Services
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