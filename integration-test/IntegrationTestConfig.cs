using System;
using ProximaX.Sirius.Storage.SDK.Connections;

namespace IntegrationTests
{
    public static class IntegrationTestConfig
    {
        public const string IpfsApiHost = "ipfs1-dev.xpxsirius.io";
        public const int IpfsApiPort = 443;
        public const HttpProtocol IpfsApiProtocol = HttpProtocol.Https;
 
        public const string BlockchainApiHost = "bctestnet4.xpxsirius.io";
        public const int BlockchainApiPort = 443;
        public const HttpProtocol BlockchainApiProtocol = HttpProtocol.Https;
        public static readonly string BlockchainRestApiUrl =
            new UriBuilder("http", BlockchainApiHost, BlockchainApiPort).Uri.AbsoluteUri;

        public const string StorageNodeApiHost = "127.0.0.1";
        public const int StorageNodeApiPort = 8081;
        public const HttpProtocol StorageNodeApiProtocol = HttpProtocol.Http;
        public const string StorageNodeApiBearer = "11111";
        public const string StorageNodeApiNemAddress = "nem:test";

        public const string AccountPrivateKey1 = "6E638FDB80CD04A9070AAB5BA966D77DFB215B145186873392B504B3B04532F2";
        public const string AccountPublicKey1 = "60CD2C2F2954FC7D97F52B7F75A66C6B44E12DC3533BAA6E78702B8CF0ABE840";
        public const string AccountAddress1 = "VARBGLTGMJKK7JSHD7QSS2YX4T6HHJHZERP5D2UL";

        public const string AccountPrivateKey2 = "A97B139EB641BCC841A610231870925EB301BA680D07BBCF9AEE83FAA5E9FB43";
        public const string AccountPublicKey2 = "817B7F9907BA72369954463A866951C010AAEFDC6A53B44D7C7691A235E83B26";
        public const string AccountAddress2 = "VCS45IZJPJKWYUZLP5PWUNAJQV2BLM3INRBHXASZ";
    }
}