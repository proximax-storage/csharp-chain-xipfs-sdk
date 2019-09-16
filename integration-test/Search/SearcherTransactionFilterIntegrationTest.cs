using ProximaX.Sirius.Storage.SDK.Connections;
using ProximaX.Sirius.Storage.SDK.Models;
using ProximaX.Sirius.Storage.SDK.Search;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static IntegrationTests.IntegrationTestConfig;

namespace IntegrationTests.Search
{
    [TestClass]
    public class SearcherTransactionFilterIntegrationTest
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
        public void ShouldSearchOutgoingTransactions()
        {
            var param = SearchParameter.CreateForAddress(AccountAddress1)
                .WithTransactionFilter(TransactionFilter.Outgoing)
                .Build();

            var result = UnitUnderTest.Search(param);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Results);
            Assert.AreEqual(result.Results.Count, 10);
        }

        [TestMethod, Timeout(10000)]
        public void ShouldSearchIncomingTransactions()
        {
            var param = SearchParameter.CreateForAddress(AccountAddress1)
                .WithTransactionFilter(TransactionFilter.Incoming)
                .Build();

            var result = UnitUnderTest.Search(param);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Results);
            Assert.AreEqual(result.Results.Count, 10);
        }

        [TestMethod, Timeout(10000)]
        public void ShouldSearchAllTransactions()
        {
            var param = SearchParameter.CreateForAddress(AccountAddress1)
                .WithTransactionFilter(TransactionFilter.All)
                .Build();

            var result = UnitUnderTest.Search(param);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Results);
            Assert.AreEqual(result.Results.Count, 10);
        }
    }
}