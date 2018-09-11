using io.nem2.sdk.Model.Blockchain;
using IO.Proximax.SDK.Models;

namespace IO.Proximax.SDK.Connections
{
    public class BlockchainNetworkConnection
    {
        public NetworkType.Types NetworkType { get; }
        public string EndpointUrl { get; }

        public BlockchainNetworkConnection(BlockchainNetworkType blockchainNetworkType, string endpointUrl)
        {
            NetworkType = blockchainNetworkType.GetNetworkType();
            EndpointUrl = endpointUrl;
        }

    }
}
