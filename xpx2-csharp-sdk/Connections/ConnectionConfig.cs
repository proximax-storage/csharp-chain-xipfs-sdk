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
            // TODO
            return new ConnectionConfig(null, storageConnection);
        }

        public static ConnectionConfig CreateWithStorageConnection(
            BlockchainNetworkConnection blockchainNetworkConnection, StorageConnection storageConnection)
        {
            return new ConnectionConfig(blockchainNetworkConnection, storageConnection);
        }
    }
}