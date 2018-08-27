using io.nem2.sdk.Model.Blockchain;
using System;
using System.Collections.Generic;
using System.Text;

namespace IO.Proximax.SDK.Connections
{
    public class BlockchainNetworkConnection
    {
        public NetworkType.Types NetworkType { get; }
        public string EndpointUrl { get; }

        public BlockchainNetworkConnection(NetworkType.Types networkType, string endpointUrl)
        {
            NetworkType = networkType;
            EndpointUrl = endpointUrl;
        }

    }
}
