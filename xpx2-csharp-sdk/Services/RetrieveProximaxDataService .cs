using System;
using System.IO;
using System.Reactive.Linq;
using IO.Proximax.SDK.Connections;
using IO.Proximax.SDK.Exceptions;
using IO.Proximax.SDK.PrivacyStrategies;
using IO.Proximax.SDK.Services.Clients;
using IO.Proximax.SDK.Utils;
using static IO.Proximax.SDK.Models.Constants;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

namespace IO.Proximax.SDK.Services
{
    public class RetrieveProximaxDataService
    {
        private IpfsApiClient IpfsApiClient { get; }
        private DigestUtils DigestUtils { get; }

        public RetrieveProximaxDataService(IpfsConnection ipfsConnection)
        {
            IpfsApiClient = new IpfsApiClient(ipfsConnection);
            DigestUtils = new DigestUtils();
        }

        internal RetrieveProximaxDataService(IpfsApiClient ipfsApiClient, DigestUtils digestUtils)
        {
            IpfsApiClient = ipfsApiClient;
            DigestUtils = digestUtils;
        }

        public IObservable<Stream> GetDataByteStream(string dataHash, IPrivacyStrategy privacyStrategy, bool validateDigest,
            string digest, string contentType) {
            
            CheckParameter(dataHash != null, "dataHash is required");
            CheckParameter(privacyStrategy != null, "privacyStrategy is required");

            if (contentType != null && contentType.Equals(PathUploadContentType)) { // path
                throw new DownloadForDataTypeNotSupportedException("download of path is not yet supported");
            } else { // byte array
                return IpfsApiClient.GetByteStream(dataHash)
                    .SelectMany(undecryptedStream => ValidateDigest(validateDigest, digest, dataHash)
                        .Select(result => undecryptedStream))
                    .Select(privacyStrategy.DecryptStream);
            }
        }

        private IObservable<bool> ValidateDigest(bool validateDigest, string digest, string dataHash) {
            return validateDigest ? IpfsApiClient.GetByteStream(dataHash)
                .SelectMany(undecryptedStream => DigestUtils.ValidateDigest(undecryptedStream, digest)) : Observable.Return(true);
        }

    }
}
