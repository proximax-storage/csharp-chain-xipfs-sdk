using io.nem2.sdk.Model.Blockchain;
using IO.Proximax.SDK.Models;

namespace IO.Proximax.SDK.Connections
{
    public class BlockchainNetworkConnection
    {
        public NetworkType.Types NetworkType { get; }
        public string RestApiUrl { get; }

        public BlockchainNetworkConnection(BlockchainNetworkType blockchainNetworkType, string restApiUrl)
        {
            NetworkType = blockchainNetworkType.GetNetworkType();
            RestApiUrl = restApiUrl;
        }

    }
}
