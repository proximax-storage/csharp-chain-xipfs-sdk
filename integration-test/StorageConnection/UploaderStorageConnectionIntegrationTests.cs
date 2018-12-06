using Proximax.Storage.SDK.Connections;
using Proximax.Storage.SDK.Models;
using Proximax.Storage.SDK.Upload;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static IntegrationTests.IntegrationTestConfig;
using static IntegrationTests.TestSupport.Constants;
using static IntegrationTests.TestDataRepository;

namespace IntegrationTests.StorageConnection
{
    [TestClass]
    public class UploaderStorageConnectionIntegrationTests
    {
        private Uploader UnitUnderTest { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            UnitUnderTest = new Uploader(
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
        public void ShouldUploadToStorageConnection()
        {
            var param = UploadParameter.CreateForStringUpload(TestString, AccountPrivateKey1).Build();

            var result = UnitUnderTest.Upload(param);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data.DataHash);

            LogAndSaveResult(result, GetType().Name + ".ShouldUploadToStorageConnection");
        }
    }
}