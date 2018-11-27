using System.IO;
using System.Security.Cryptography;
using IO.Proximax.SDK.Connections;
using IO.Proximax.SDK.Download;
using IO.Proximax.SDK.Models;
using IO.Proximax.SDK.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static IntegrationTests.IntegrationTestConfig;
using static IntegrationTests.TestSupport.Constants;

namespace IntegrationTests.Download
{
    [TestClass]
    public class DownloaderDownloadPrivacyStrategyIntegrationTest
    {
        private Downloader UnitUnderTest { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            UnitUnderTest = new Downloader(
                ConnectionConfig.CreateWithLocalIpfsConnection(
                    new BlockchainNetworkConnection(BlockchainNetworkType.MijinTest, BlockchainApiHost, BlockchainApiPort, BlockchainApiProtocol),
                    new IpfsConnection(IpfsApiHost, IpfsApiPort, BlockchainApiProtocol))
            );
        }

	    [TestMethod, Timeout(10000)]
	    public void ShouldDownloadWithPlainPrivacy() {
		    var transactionHash = TestDataRepository
			    .GetData("UploaderPrivacyStrategyIntegrationTests.ShouldUploadFileWithPlainPrivacyStrategy", "transactionHash");
		    var param = DownloadParameter.Create(transactionHash)
			    .WithPlainPrivacy()
			    .Build();
	
		    var result = UnitUnderTest.Download(param);
	
		    Assert.IsNotNull(result);
		    Assert.AreEqual(result.PrivacyType, (int) PrivacyType.Plain);
		    Assert.AreEqual(result.Data.GetByteStream().GetContentAsString(), new FileStream(TestTextFile, FileMode.Open, FileAccess.Read).GetContentAsString());
	    }
	    
	    [TestMethod, Timeout(10000)]
	    public void ShouldDownloadWithNemKeysPrivacy() {
		    var transactionHash = TestDataRepository
			    .GetData("UploaderPrivacyStrategyIntegrationTests.ShouldUploadFileWithSecuredWithNemKeysPrivacyStrategy", "transactionHash");
		    var param = DownloadParameter.Create(transactionHash)
			    .WithNemKeysPrivacy(PrivateKey1, PublicKey2)
			    .Build();
	
		    var result = UnitUnderTest.Download(param);
	
		    Assert.IsNotNull(result);
		    Assert.AreEqual(result.PrivacyType, (int) PrivacyType.NemKeys);
		    Assert.AreEqual(result.Data.GetByteStream().GetContentAsString(), new FileStream(TestTextFile, FileMode.Open, FileAccess.Read).GetContentAsString());
	    }
	    
	    [TestMethod, Timeout(10000), ExpectedException(typeof(CryptographicException))]
	    public void FailDownloadWithIncorrectNemKeys() {
		    var transactionHash = TestDataRepository
			    .GetData("UploaderPrivacyStrategyIntegrationTests.ShouldUploadFileWithSecuredWithNemKeysPrivacyStrategy", "transactionHash");
		    var param = DownloadParameter.Create(transactionHash)
			    .WithNemKeysPrivacy(PrivateKey1, PublicKey1)
			    .Build();
	
		    var result = UnitUnderTest.Download(param);
	
		    result.Data.GetByteStream().GetContentAsByteArray();
	    }
	    
	    [TestMethod, Timeout(10000)]
	    public void ShouldDownloadWithPasswordPrivacy() {
		    var transactionHash = TestDataRepository
			    .GetData("UploaderPrivacyStrategyIntegrationTests.ShouldUploadFileWithSecuredWithPasswordPrivacyStrategy", "transactionHash");
		    var param = DownloadParameter.Create(transactionHash)
			    .WithPasswordPrivacy(TestPassword)
			    .Build();
	
		    var result = UnitUnderTest.Download(param);
	
		    Assert.IsNotNull(result);
		    Assert.AreEqual(result.PrivacyType, (int) PrivacyType.Password);
		    Assert.AreEqual(result.Data.GetByteStream().GetContentAsString(), new FileStream(TestTextFile, FileMode.Open, FileAccess.Read).GetContentAsString());
	    }
	    
	    [TestMethod, Timeout(10000), ExpectedException(typeof(CryptographicException))]
	    public void FailDownloadWithIncorrectPassword() {
		    var transactionHash = TestDataRepository
			    .GetData("UploaderPrivacyStrategyIntegrationTests.ShouldUploadFileWithSecuredWithPasswordPrivacyStrategy", "transactionHash");
		    var param = DownloadParameter.Create(transactionHash)
			    .WithPasswordPrivacy(TestPassword + "dummy")
			    .Build();
	
		    var result = UnitUnderTest.Download(param);
	
		    result.Data.GetByteStream().GetContentAsByteArray();
	    }
	    
   }
}