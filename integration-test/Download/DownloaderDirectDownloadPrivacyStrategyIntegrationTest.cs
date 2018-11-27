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
    public class DownloaderDirectDownloadPrivacyStrategyIntegrationTest
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
	    public void ShouldDirectDownloadUsingTransactionHashWithPlainPrivacy() {
		    var transactionHash = TestDataRepository
			    .GetData("UploaderPrivacyStrategyIntegrationTests.ShouldUploadFileWithPlainPrivacyStrategy", "transactionHash");
		    var param = DirectDownloadParameter.CreateFromTransactionHash(transactionHash)
			    .WithPlainPrivacy()
			    .Build();
	
		    var result = UnitUnderTest.DirectDownload(param);
	
		    Assert.IsNotNull(result);
		    Assert.AreEqual(result.GetContentAsString(), new FileStream(TestTextFile, FileMode.Open, FileAccess.Read).GetContentAsString());
	    }
	    
	    [TestMethod, Timeout(10000)]
	    public void ShouldDirectDownloadUsingTransactionHashWithNemKeysPrivacy() {
		    var transactionHash = TestDataRepository
			    .GetData("UploaderPrivacyStrategyIntegrationTests.ShouldUploadFileWithSecuredWithNemKeysPrivacyStrategy", "transactionHash");
		    var param = DirectDownloadParameter.CreateFromTransactionHash(transactionHash)
			    .WithNemKeysPrivacy(PrivateKey1, PublicKey2)
			    .Build();
	
		    var result = UnitUnderTest.DirectDownload(param);
	
		    Assert.IsNotNull(result);
		    Assert.AreEqual(result.GetContentAsString(), new FileStream(TestTextFile, FileMode.Open, FileAccess.Read).GetContentAsString());
	    }
	    
	    [TestMethod, Timeout(10000), ExpectedException(typeof(CryptographicException))]
	    public void FailDirectDownloadUsingTransactionHashWithIncorrectNemKeys() {
		    var transactionHash = TestDataRepository
			    .GetData("UploaderPrivacyStrategyIntegrationTests.ShouldUploadFileWithSecuredWithNemKeysPrivacyStrategy", "transactionHash");
		    var param = DirectDownloadParameter.CreateFromTransactionHash(transactionHash)
			    .WithNemKeysPrivacy(PrivateKey1, PublicKey1)
			    .Build();
	
		    var result = UnitUnderTest.DirectDownload(param);
	
		    result.GetContentAsByteArray();
	    }
	    
	    [TestMethod, Timeout(10000)]
	    public void ShouldDirectDownloadUsingTransactionHashWithPasswordPrivacy() {
		    var transactionHash = TestDataRepository
			    .GetData("UploaderPrivacyStrategyIntegrationTests.ShouldUploadFileWithSecuredWithPasswordPrivacyStrategy", "transactionHash");
		    var param = DirectDownloadParameter.CreateFromTransactionHash(transactionHash)
			    .WithPasswordPrivacy(TestPassword)
			    .Build();
	
		    var result = UnitUnderTest.DirectDownload(param);
	
		    Assert.IsNotNull(result);
		    Assert.AreEqual(result.GetContentAsString(), new FileStream(TestTextFile, FileMode.Open, FileAccess.Read).GetContentAsString());
	    }
	    
	    [TestMethod, Timeout(10000), ExpectedException(typeof(CryptographicException))]
	    public void FailDirectDownloadUsingTransactionHashWithIncorrectPassword() {
		    var transactionHash = TestDataRepository
			    .GetData("UploaderPrivacyStrategyIntegrationTests.ShouldUploadFileWithSecuredWithPasswordPrivacyStrategy", "transactionHash");
		    var param = DirectDownloadParameter.CreateFromTransactionHash(transactionHash)
			    .WithPasswordPrivacy(TestPassword + "dummy")
			    .Build();
	
		    var result = UnitUnderTest.DirectDownload(param);
	
		    result.GetContentAsByteArray();
	    }
	    
 	    [TestMethod, Timeout(10000)]
	    public void ShouldDirectDownloadUsingDataHashWithPlainPrivacy() {
		    var dataHash = TestDataRepository
			    .GetData("UploaderPrivacyStrategyIntegrationTests.ShouldUploadFileWithPlainPrivacyStrategy", "dataHash");
		    var param = DirectDownloadParameter.CreateFromDataHash(dataHash)
			    .WithPlainPrivacy()
			    .Build();
	
		    var result = UnitUnderTest.DirectDownload(param);
	
		    Assert.IsNotNull(result);
		    Assert.AreEqual(result.GetContentAsString(), new FileStream(TestTextFile, FileMode.Open, FileAccess.Read).GetContentAsString());
	    }
	    
	    [TestMethod, Timeout(10000)]
	    public void ShouldDirectDownloadUsingDataHashWithNemKeysPrivacy() {
		    var dataHash = TestDataRepository
			    .GetData("UploaderPrivacyStrategyIntegrationTests.ShouldUploadFileWithSecuredWithNemKeysPrivacyStrategy", "dataHash");
		    var param = DirectDownloadParameter.CreateFromDataHash(dataHash)
			    .WithNemKeysPrivacy(PrivateKey1, PublicKey2)
			    .Build();
	
		    var result = UnitUnderTest.DirectDownload(param);
	
		    Assert.IsNotNull(result);
		    Assert.AreEqual(result.GetContentAsString(), new FileStream(TestTextFile, FileMode.Open, FileAccess.Read).GetContentAsString());
	    }
	    
	    [TestMethod, Timeout(10000), ExpectedException(typeof(CryptographicException))]
	    public void FailDirectDownloadUsingDataHashWhenIncorrectNemKeys() {
		    var dataHash = TestDataRepository
			    .GetData("UploaderPrivacyStrategyIntegrationTests.ShouldUploadFileWithSecuredWithNemKeysPrivacyStrategy", "dataHash");
		    var param = DirectDownloadParameter.CreateFromDataHash(dataHash)
			    .WithNemKeysPrivacy(PrivateKey1, PublicKey1)
			    .Build();
	
		    var result = UnitUnderTest.DirectDownload(param);
	
		    result.GetContentAsByteArray();
	    }
	    
	    [TestMethod, Timeout(10000)]
	    public void ShouldDirectDownloadUsingDataHashWithPasswordPrivacy() {
		    var dataHash = TestDataRepository
			    .GetData("UploaderPrivacyStrategyIntegrationTests.ShouldUploadFileWithSecuredWithPasswordPrivacyStrategy", "dataHash");
		    var param = DirectDownloadParameter.CreateFromDataHash(dataHash)
			    .WithPasswordPrivacy(TestPassword)
			    .Build();
	
		    var result = UnitUnderTest.DirectDownload(param);
	
		    Assert.IsNotNull(result);
		    Assert.AreEqual(result.GetContentAsString(), new FileStream(TestTextFile, FileMode.Open, FileAccess.Read).GetContentAsString());
	    }
	    
	    [TestMethod, Timeout(10000), ExpectedException(typeof(CryptographicException))]
	    public void FailDirectDownloadUsingDataHashWhenIncorrectPassword() {
		    var dataHash = TestDataRepository
			    .GetData("UploaderPrivacyStrategyIntegrationTests.ShouldUploadFileWithSecuredWithPasswordPrivacyStrategy", "dataHash");
		    var param = DirectDownloadParameter.CreateFromDataHash(dataHash)
			    .WithPasswordPrivacy(TestPassword + "dummy")
			    .Build();
	
		    var result = UnitUnderTest.DirectDownload(param);
	
		    result.GetContentAsByteArray();
	    }
	    
   }
}