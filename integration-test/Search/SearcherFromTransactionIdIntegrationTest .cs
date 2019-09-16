using ProximaX.Sirius.Storage.SDK.Connections;
using ProximaX.Sirius.Storage.SDK.Models;
using ProximaX.Sirius.Storage.SDK.Search;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static IntegrationTests.IntegrationTestConfig;

namespace IntegrationTests.Search
{
    [TestClass]
    public class SearcherFromTransactionIdIntegrationTest
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
        public void ShouldSearchWithFromTransactionId()
        {
            var param = SearchParameter.CreateForAddress("SDB5DP6VGVNPSQJYEC2X3QIWKAFJ3DCMNQCIF6OA")
                .WithFromTransactionId("5BDEDB565D6F4B0001155AFF")
                .Build();

            var result = UnitUnderTest.Search(param);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Results);
            Assert.AreEqual(result.Results.Count, 10);
            Assert.AreEqual(result.Results[0].TransactionHash,
                "471C6C6A3EAC3F18849C1CA2C06D532AA2A83D7E89D99858F52DEE6DCD0D3762");
            Assert.IsNotNull(result.ToTransactionId);
            Assert.AreEqual(result.FromTransactionId, "5BDEDB565D6F4B0001155AFF");
        }
    }
}