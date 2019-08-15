using System;
using ProximaX.Sirius.Storage.SDK.Connections;

namespace IntegrationTests
{
    public static class IntegrationTestConfig
    {
        public const string IpfsApiHost = "ipfs1-dev.xpxsirius.io";
        public const int IpfsApiPort = 5001;
        public const HttpProtocol IpfsApiProtocol = HttpProtocol.Http;
 
        public const string BlockchainApiHost = "192.168.0.18";
        public const int BlockchainApiPort = 3000;
        public const HttpProtocol BlockchainApiProtocol = HttpProtocol.Http;
        public static readonly string BlockchainRestApiUrl =
            new UriBuilder("http", BlockchainApiHost, BlockchainApiPort).Uri.AbsoluteUri;

        public const string StorageNodeApiHost = "127.0.0.1";
        public const int StorageNodeApiPort = 8081;
        public const HttpProtocol StorageNodeApiProtocol = HttpProtocol.Http;
        public const string StorageNodeApiBearer = "11111";
        public const string StorageNodeApiNemAddress = "nem:test";

        public const string AccountPrivateKey1 = "2C8178EF9ED7A6D30ABDC1E4D30D68B05861112A98B1629FBE2C8D16FDE97A1C";
        public const string AccountPublicKey1 = "73472A2E9DCEA5C2A36EB7F6A34A634010391EC89E883D67360DB16F28B9443C";
        public const string AccountAddress1 = "SAFSPPRI4MBM3R7USYLJHUODAD5ZEK65YUP35NV6";

        public const string AccountPrivateKey2 = "A97B139EB641BCC841A610231870925EB301BA680D07BBCF9AEE83FAA5E9FB43";
        public const string AccountPublicKey2 = "68F50E10E5B8BE2B7E9DDB687A667D6E94DD55FE02B4AED8195F51F9A242558B";
        public const string AccountAddress2 = "SCPNZICWMYWVHVSYQHJ65EMKWYT7FZSPAKP4UPEN";
    }
}