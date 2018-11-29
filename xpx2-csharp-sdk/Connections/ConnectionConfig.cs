using System.Reactive.Linq;
using IO.Proximax.SDK.Models;
using IO.Proximax.SDK.Services.Clients;

namespace IO.Proximax.SDK.Connections
{
    public class ConnectionConfig
    {
        public BlockchainNetworkConnection BlockchainNetworkConnection { get; }
        public IFileStorageConnection FileStorageConnection { get; }

        private ConnectionConfig(BlockchainNetworkConnection blockchainNetworkConnection,
            IFileStorageConnection fileStorageConnection)
        {
            BlockchainNetworkConnection = blockchainNetworkConnection;
            FileStorageConnection = fileStorageConnection;
        }

        public static ConnectionConfig CreateWithLocalIpfsConnection(
            BlockchainNetworkConnection blockchainNetworkConnection, IpfsConnection ipfsConnection)
        {
            return new ConnectionConfig(blockchainNetworkConnection, ipfsConnection);
        }

        public static ConnectionConfig CreateWithStorageConnection(StorageConnection storageConnection)
        {
            var storageNodeClient = new StorageNodeClient(storageConnection);
            var blockchainNetwork = storageNodeClient.GetNodeInfo().Wait().BlockchainNetwork;
            var blockchainNetworkConnection = new BlockchainNetworkConnection(
                BlockchainNetworkTypeConverter.GetNetworkType(blockchainNetwork.NetworkType),
                blockchainNetwork.Host,
                blockchainNetwork.Port,
               HttpProtocolConverter.GetHttpProtocol(blockchainNetwork.Protocol)
            );
            return new ConnectionConfig(blockchainNetworkConnection, storageConnection);
        }

        public static ConnectionConfig CreateWithStorageConnection(
            BlockchainNetworkConnection blockchainNetworkConnection, StorageConnection storageConnection)
        {
            return new ConnectionConfig(blockchainNetworkConnection, storageConnection);
        }
    }
}