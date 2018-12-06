using Proximax.Storage.SDK.Connections;
using Proximax.Storage.SDK.Download;
using Proximax.Storage.SDK.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static IntegrationTests.IntegrationTestConfig;
using static IntegrationTests.TestSupport.Constants;

namespace IntegrationTests.StorageConnection
{
    [TestClass]
    public class DownloaderDownloadStorageConnectionIntegrationTests
    {
        private Downloader UnitUnderTest { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            UnitUnderTest = new Downloader(
                ConnectionConfig.CreateWithStorageConnection(
                    new BlockchainNetworkConnection(BlockchainNetworkType.MijinTest, BlockchainApiHost,
                        BlockchainApiPort, BlockchainApiProtocol),
                    new Proximax.Storage.SDK.Connections.StorageConnection(
                        StorageNodeApiHost,
                        StorageNodeApiPort,
                        StorageNodeApiProtocol,
                        StorageNodeApiBearer,
                        StorageNodeApiNemAddress))
            );
        }

        [TestMethod, Timeout(30000)]
        public void ShouldDownloadFromStorageConnection()
        {
            var transactionHash = TestDataRepository
                .GetData("UploaderStorageConnectionIntegrationTests.ShouldUploadToStorageConnection",
                    "transactionHash");
            var param = DownloadParameter.Create(transactionHash).Build();

            var result = UnitUnderTest.Download(param);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Data.GetContentAsString(), TestString);
        }
    }
}