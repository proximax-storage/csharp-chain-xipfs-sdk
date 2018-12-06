using System;
using Proximax.Storage.SDK.Connections;

namespace IntegrationTests
{
    public static class IntegrationTestConfig
    {
        public const string IpfsApiHost = "ipfs1-dev.xpxsirius.io";
        public const int IpfsApiPort = 5001;
        public const HttpProtocol IpfsApiProtocol = HttpProtocol.Http;
 
        public const string BlockchainApiHost = "privatetest1.xpxsirius.io";
        public const int BlockchainApiPort = 3000;
        public const HttpProtocol BlockchainApiProtocol = HttpProtocol.Http;
        public static readonly string BlockchainRestApiUrl =
            new UriBuilder("http", BlockchainApiHost, BlockchainApiPort).Uri.AbsoluteUri;

        public const string StorageNodeApiHost = "127.0.0.1";
        public const int StorageNodeApiPort = 8081;
        public const HttpProtocol StorageNodeApiProtocol = HttpProtocol.Http;
        public const string StorageNodeApiBearer = "11111";
        public const string StorageNodeApiNemAddress = "nem:test";

        public const string AccountPrivateKey1 = "97226FCCBD876D399BA2A70E640AD2C2C97AD5CE57A40EE9455C226D3C39AD49";
        public const string AccountPublicKey1 = "632479641258F56F961473CD729F6357563D276CE7B68D5AD8F9F7FA071BB963";
        public const string AccountAddress1 = "SDB5DP6VGVNPSQJYEC2X3QIWKAFJ3DCMNQCIF6OA";

        public const string AccountPrivateKey2 = "D19EDBF7C5F4665BBB168F8BFF3DC1CA85766080B10AABD60DDE5D6D7E893D5B";
        public const string AccountPublicKey2 = "D1869362F4FAA5F683AEF78FC0D6E04B976833000F3958862A09CC7B6DF347C2";
        public const string AccountAddress2 = "SDUCJBPMHXWEWJL6KI4GVW3X4EKWSINM3WBVUDQ2";
    }
}