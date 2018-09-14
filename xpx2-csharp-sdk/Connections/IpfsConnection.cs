using Ipfs.Api;

namespace IO.Proximax.SDK.Connections
{
    public class IpfsConnection
    {
        public IpfsClient Ipfs { get; }

        public IpfsConnection(string ipfsUrl)
        {
            Ipfs = new IpfsClient(ipfsUrl);
        }
    }
}
