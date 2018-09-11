using System;
using System.IO;
using System.Reactive.Linq;
using IO.Proximax.SDK.Connections;
using IO.Proximax.SDK.Services.Clients;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

namespace IO.Proximax.SDK.Services
{
    public class IpfsUploadService
    {
        private IpfsApiClient IpfsApiClient { get; }

        public IpfsUploadService(IpfsConnection ipfsConnection)
        {
            IpfsApiClient = new IpfsApiClient(ipfsConnection);
        }

        public IpfsUploadService(IpfsApiClient ipfsApiClient)
        {
            IpfsApiClient = ipfsApiClient;
        }
        
        public IObservable<IpfsUploadResponse> UploadByteStream(Stream byteStream) {
            CheckParameter(byteStream != null, "byteStream is required");

            return IpfsApiClient.AddByteStream(byteStream)
                .Select(dataHash => new IpfsUploadResponse(dataHash, CurrentTimeMillis()));
        }

        public IObservable<IpfsUploadResponse> UploadPath(string path) {
            CheckParameter(path != null, "path is required");

            return IpfsApiClient.AddPath(path)
                .Select(dataHash => new IpfsUploadResponse(dataHash, CurrentTimeMillis()));
        }

        private long CurrentTimeMillis()
        {
            return (long)(DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds;
        }
    }
}
