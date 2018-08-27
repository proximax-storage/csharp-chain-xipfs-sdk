using System;
using System.Collections.Generic;
using System.Text;
using Ipfs.Api;

namespace IO.Proximax.SDK.Connections
{
    public class IpfsConnection
    {
        public IpfsClient Ipfs { get; }

        public IpfsConnection(string multiAddr)
        {
            Ipfs = new IpfsClient(multiAddr);
        }
    }
}
