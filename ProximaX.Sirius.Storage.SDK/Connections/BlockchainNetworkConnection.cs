using System;
using ProximaX.Sirius.Storage.SDK.Models;
using ProximaX.Sirius.Chain.Sdk.Model.Blockchain;
using static ProximaX.Sirius.Storage.SDK.Utils.ParameterValidationUtils;

namespace ProximaX.Sirius.Storage.SDK.Connections
{
    public class BlockchainNetworkConnection
    {
        public NetworkType NetworkType { get; }
        public string RestApiUrl { get; }
        public string ApiHost { get; }
        public int ApiPort { get; }
        public HttpProtocol HttpProtocol { get; }

        public string GenerationHash { get; }
        public BlockchainNetworkConnection(BlockchainNetworkType networkType, string apiHost = "localhost",
            int apiPort = 3000,
            HttpProtocol apiProtocol = HttpProtocol.Http)
        {
            CheckParameter(apiHost != null, "apiHost is required");
            CheckParameter(apiPort > 0, "apiPort must be non-negative int");

            NetworkType = networkType.GetNetworkType();
            ApiHost = apiHost;
            ApiPort = apiPort;
            HttpProtocol = apiProtocol;
            RestApiUrl = new UriBuilder(HttpProtocol.GetProtocol(), apiHost, apiPort).Uri.AbsoluteUri.TrimEnd('/');
         
        }
    }
}