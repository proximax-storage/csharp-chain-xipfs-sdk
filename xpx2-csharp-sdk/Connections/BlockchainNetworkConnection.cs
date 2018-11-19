using System;
using io.nem2.sdk.Model.Blockchain;
using IO.Proximax.SDK.Models;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

namespace IO.Proximax.SDK.Connections
{
    public class BlockchainNetworkConnection
    {
        public NetworkType.Types NetworkType { get; }
        public string RestApiUrl { get; }
        public string ApiHost { get; }
        public int ApiPort { get; }
        public HttpProtocol HttpProtocol { get; }

        public BlockchainNetworkConnection(BlockchainNetworkType networkType, string apiHost = "localhost", int apiPort = 3000,
            HttpProtocol apiProtocol = HttpProtocol.Http)
        {
            CheckParameter(networkType != null, "networkType is required");
            CheckParameter(apiHost != null, "apiHost is required");
            CheckParameter(apiPort > 0, "apiPort must be non-negative int");
            CheckParameter(apiProtocol != null, "apiProtocol is required");

            NetworkType = networkType.GetNetworkType();
            ApiHost = apiHost;
            ApiPort = apiPort;
            HttpProtocol = apiProtocol;
            RestApiUrl = new UriBuilder(HttpProtocol.GetProtocol(), apiHost, apiPort).Uri.AbsoluteUri;
        }
    }
}