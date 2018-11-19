using System;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

namespace IO.Proximax.SDK.Connections
{
    public class StorageConnection : IFileStorageConnection
    {
        public string RestApiUrl { get; }
        public string ApiHost { get; }
        public int ApiPort { get; }
        public HttpProtocol HttpProtocol { get; }
        public string BearerToken { get; }
        public string NemAddress { get; }


        public StorageConnection(string apiHost, int apiPort, HttpProtocol apiProtocol, string bearerToken,
            string nemAddress)
        {
            CheckParameter(apiHost != null, "apiHost is required");
            CheckParameter(apiPort > 0, "apiPort must be non-negative int");
            CheckParameter(apiProtocol != null, "apiProtocol is required");

            ApiHost = apiHost;
            ApiPort = apiPort;
            HttpProtocol = apiProtocol;
            BearerToken = bearerToken;
            NemAddress = nemAddress;
            RestApiUrl = new UriBuilder(HttpProtocol.GetProtocol(), apiHost, apiPort).Uri.AbsoluteUri;
        }
    }
}