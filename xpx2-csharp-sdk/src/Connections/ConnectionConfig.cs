using System;
using System.Collections.Generic;
using System.Text;

namespace IO.Proximax.SDK.Connections
{
    public class ConnectionConfig
    {
        public BlockchainNetworkConnection BlockchainNetworkConnection { get; }
        public IpfsConnection IpfsConnection { get; }

        private ConnectionConfig(BlockchainNetworkConnection blockchainNetworkConnection, IpfsConnection ipfsConnection)
        {
            this.BlockchainNetworkConnection = BlockchainNetworkConnection;
            this.IpfsConnection = IpfsConnection;
        }

        public static ConnectionConfig Create(BlockchainNetworkConnection blockchainNetworkConnection, IpfsConnection ipfsConnection)
        {
            return new ConnectionConfig(blockchainNetworkConnection, ipfsConnection);
        }
    }
}
