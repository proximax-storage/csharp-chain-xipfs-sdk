using Ipfs.Api;

namespace IO.Proximax.SDK.Connections
{
    public class IpfsConnection
    {
        public IpfsClient Ipfs { get; }

        public IpfsConnection(string multiAddress)
        {
            Ipfs = new IpfsClient(multiAddress);
        }
    }
}
