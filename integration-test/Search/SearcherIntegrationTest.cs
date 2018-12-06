using Proximax.Storage.SDK.Connections;
using Proximax.Storage.SDK.Models;
using Proximax.Storage.SDK.Search;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static IntegrationTests.IntegrationTestConfig;

namespace IntegrationTests.Search
{
    [TestClass]
    public class SearcherIntegrationTest
    {
        private Searcher UnitUnderTest { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            UnitUnderTest = new Searcher(
                ConnectionConfig.CreateWithLocalIpfsConnection(
                    new BlockchainNetworkConnection(BlockchainNetworkType.MijinTest, BlockchainApiHost,
                        BlockchainApiPort, BlockchainApiProtocol),
                    new IpfsConnection(IpfsApiHost, IpfsApiPort, BlockchainApiProtocol))
            );
        }

        [TestMethod, Timeout(10000)]
        public void ShouldSearchWithAccountAddress()
        {
            var param = SearchParameter.CreateForAddress(AccountAddress1).Build();

            var result = UnitUnderTest.Search(param);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Results);
            Assert.AreEqual(result.Results.Count, 10);
            Assert.IsNotNull(result.ToTransactionId);
            Assert.IsNull(result.FromTransactionId);
        }

        [TestMethod, Timeout(10000)]
        public void ShouldSearchWithAccountPublicKey()
        {
            var param = SearchParameter.CreateForPublicKey(AccountPublicKey1).Build();

            var result = UnitUnderTest.Search(param);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Results);
            Assert.AreEqual(result.Results.Count, 10);
            Assert.IsNotNull(result.ToTransactionId);
            Assert.IsNull(result.FromTransactionId);
        }

        [TestMethod, Timeout(10000)]
        public void ShouldSearchWithAccountPrivateKey()
        {
            var param = SearchParameter.CreateForPrivateKey(AccountPrivateKey1).Build();

            var result = UnitUnderTest.Search(param);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Results);
            Assert.AreEqual(result.Results.Count, 10);
            Assert.IsNotNull(result.ToTransactionId);
            Assert.IsNull(result.FromTransactionId);
        }

        [TestMethod, Timeout(10000)]
        public void ShouldSearchAccountWithNoUploadTxns()
        {
            var param = SearchParameter
                .CreateForPrivateKey("D962E38EA1CD8E1B2583E3DB8F1113F060351300575806D5D56FE10CA234DB2A").Build();

            var result = UnitUnderTest.Search(param);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Results);
            Assert.AreEqual(result.Results.Count, 0);
            Assert.IsNull(result.ToTransactionId);
            Assert.IsNull(result.FromTransactionId);
        }
    }
}