using ProximaX.Sirius.Storage.SDK.Connections;
using ProximaX.Sirius.Storage.SDK.Download;
using ProximaX.Sirius.Storage.SDK.Exceptions;
using ProximaX.Sirius.Storage.SDK.Models;
using ProximaX.Sirius.Storage.SDK.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static IntegrationTests.IntegrationTestConfig;
using static IntegrationTests.TestSupport.Constants;

namespace IntegrationTests.Download
{
    [TestClass]
    public class DownloaderDirectDownloadSecureMessageIntegrationTest
    {
        private Downloader UnitUnderTest { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            UnitUnderTest = new Downloader(
                ConnectionConfig.CreateWithLocalIpfsConnection(
                    new BlockchainNetworkConnection(BlockchainNetworkType.MijinTest, BlockchainApiHost,
                        BlockchainApiPort, BlockchainApiProtocol),
                    new IpfsConnection(IpfsApiHost, IpfsApiPort, BlockchainApiProtocol))
            );
        }

        [TestMethod, Timeout(10000)]
        public void ShouldDirectDownloadWithSecureMessageAsSender()
        {
            var transactionHash = TestDataRepository
                .GetData(
                    "UploaderSecureMessageIntegrationTests.ShouldUploadWithUseBlockchainSecureMessageAndRecipientPublicKey",
                    "transactionHash");
            var param = DirectDownloadParameter.CreateFromTransactionHash(transactionHash, AccountPrivateKey1)
                .Build();

            var result = UnitUnderTest.DirectDownload(param);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetContentAsString(), TestString);
        }

        [TestMethod, Timeout(10000)]
        public void ShouldDirectDownloadWithSecureMessageAsRecipient()
        {
            var transactionHash = TestDataRepository
                .GetData(
                    "UploaderSecureMessageIntegrationTests.ShouldUploadWithUseBlockchainSecureMessageAndRecipientPublicKey",
                    "transactionHash");
            var param = DirectDownloadParameter.CreateFromTransactionHash(transactionHash, AccountPrivateKey1)
                .Build();

            var result = UnitUnderTest.DirectDownload(param);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetContentAsString(), TestString);
        }

        [TestMethod, Timeout(10000), ExpectedException(typeof(DirectDownloadFailureException))]
        public void FailDirectDownloadWithWrongPrivateKey()
        {
            var transactionHash = TestDataRepository
                .GetData(
                    "UploaderSecureMessageIntegrationTests.ShouldUploadWithUseBlockchainSecureMessageAndRecipientPublicKey",
                    "transactionHash");
            var param = DirectDownloadParameter.CreateFromTransactionHash(transactionHash,
                    "7FE209FAA5DE3E1EDEBD169091145788FF7B0847AD2FE04FB7706A660BFCAF0A")
                .Build();

            UnitUnderTest.DirectDownload(param);
        }

        [TestMethod, Timeout(10000), ExpectedException(typeof(DirectDownloadFailureException))]
        public void FailDirectDownloadWithNoPrivateKey()
        {
            var transactionHash = TestDataRepository
                .GetData(
                    "UploaderSecureMessageIntegrationTests.ShouldUploadWithUseBlockchainSecureMessageAndRecipientPublicKey",
                    "transactionHash");
            var param = DirectDownloadParameter.CreateFromTransactionHash(transactionHash)
                .Build();

            UnitUnderTest.DirectDownload(param);
        }
    }
}