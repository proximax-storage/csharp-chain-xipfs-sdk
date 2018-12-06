using System.IO;
using Proximax.Storage.SDK.Connections;
using Proximax.Storage.SDK.Models;
using Proximax.Storage.SDK.PrivacyStrategies;
using Proximax.Storage.SDK.Upload;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static IntegrationTests.IntegrationTestConfig;
using static IntegrationTests.TestDataRepository;
using static IntegrationTests.TestSupport.Constants;

namespace IntegrationTests.Upload
{
    [TestClass]
    public class UploaderPrivacyStrategyIntegrationTests
    {
        private Uploader UnitUnderTest { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            UnitUnderTest = new Uploader(
                ConnectionConfig.CreateWithLocalIpfsConnection(
                    new BlockchainNetworkConnection(BlockchainNetworkType.MijinTest, BlockchainApiHost,
                        BlockchainApiPort, BlockchainApiProtocol),
                    new IpfsConnection(IpfsApiHost, IpfsApiPort, BlockchainApiProtocol))
            );
        }

        [TestMethod, Timeout(30000)]
        public void ShouldUploadFileWithPlainPrivacyStrategy()
        {
            var param = UploadParameter.CreateForFileUpload(TestTextFile, AccountPrivateKey1)
                .WithPlainPrivacy()
                .Build();

            var result = UnitUnderTest.Upload(param);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.TransactionHash);
            Assert.IsNotNull(result.Data.DataHash);
            Assert.AreEqual(result.PrivacyType, (int) PrivacyType.Plain);

            LogAndSaveResult(result, GetType().Name + ".ShouldUploadFileWithPlainPrivacyStrategy");
        }

        [TestMethod, Timeout(30000)]
        public void ShouldUploadFileWithSecuredWithNemKeysPrivacyStrategy()
        {
            var param = UploadParameter.CreateForFileUpload(TestTextFile, AccountPrivateKey1)
                .WithNemKeysPrivacy(AccountPrivateKey1, AccountPublicKey2)
                .Build();

            var result = UnitUnderTest.Upload(param);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.TransactionHash);
            Assert.IsNotNull(result.Data.DataHash);
            Assert.AreEqual(result.PrivacyType, (int) PrivacyType.NemKeys);

            LogAndSaveResult(result, GetType().Name + ".ShouldUploadFileWithSecuredWithNemKeysPrivacyStrategy");
        }

        [TestMethod, Timeout(30000)]
        public void ShouldUploadFileWithSecuredWithPasswordPrivacyStrategy()
        {
            var param = UploadParameter.CreateForFileUpload(TestTextFile, AccountPrivateKey1)
                .WithPasswordPrivacy(TestPassword)
                .Build();

            var result = UnitUnderTest.Upload(param);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.TransactionHash);
            Assert.IsNotNull(result.Data.DataHash);
            Assert.AreEqual(result.PrivacyType, (int) PrivacyType.Password);

            LogAndSaveResult(result, GetType().Name + ".ShouldUploadFileWithSecuredWithPasswordPrivacyStrategy");
        }

        [TestMethod, Timeout(30000)]
        public void ShouldUploadFileWithCustomPrivacyStrategy()
        {
            var param = UploadParameter.CreateForFileUpload(TestTextFile, AccountPrivateKey1)
                .WithPrivacyStrategy(new MyPrivacyStrategy())
                .Build();

            var result = UnitUnderTest.Upload(param);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.TransactionHash);
            Assert.IsNotNull(result.Data.DataHash);
            Assert.AreEqual(result.PrivacyType, (int) PrivacyType.Custom);

            LogAndSaveResult(result, GetType().Name + ".ShouldUploadFileWithCustomPrivacyStrategy");
        }
    }

    public class MyPrivacyStrategy : ICustomPrivacyStrategy
    {
        public override Stream EncryptStream(Stream data)
        {
            return data;
        }

        public override Stream DecryptStream(Stream data)
        {
            return data;
        }
    }
}