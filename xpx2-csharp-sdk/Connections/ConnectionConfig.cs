namespace IO.Proximax.SDK.Connections
{
    public class ConnectionConfig
    {
        public BlockchainNetworkConnection BlockchainNetworkConnection { get; }
        public IpfsConnection IpfsConnection { get; }

        private ConnectionConfig(BlockchainNetworkConnection blockchainNetworkConnection, IpfsConnection ipfsConnection)
        {
            BlockchainNetworkConnection = blockchainNetworkConnection;
            IpfsConnection = ipfsConnection;
        }

        public static ConnectionConfig Create(BlockchainNetworkConnection blockchainNetworkConnection, IpfsConnection ipfsConnection)
        {
            return new ConnectionConfig(blockchainNetworkConnection, ipfsConnection);
        }
    }
}
