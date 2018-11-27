using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IO.Proximax.SDK.Connections;
using IO.Proximax.SDK.Download;
using IO.Proximax.SDK.Exceptions;
using IO.Proximax.SDK.Models;
using IO.Proximax.SDK.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static IntegrationTests.IntegrationTestConfig;
using static IO.Proximax.SDK.Models.Constants;
using static IntegrationTests.TestSupport.Constants;

namespace IntegrationTests.Download
{
    [TestClass]
    public class DownloaderDownloadSecureMessageIntegrationTest
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
		public void ShouldDownloadWithSecureMessageAsSender() {
			var transactionHash = TestDataRepository
					.GetData("UploaderSecureMessageIntegrationTests.ShouldUploadWithUseBlockchainSecureMessageAndRecipientPublicKey", "transactionHash");
			var param = DownloadParameter.Create(transactionHash)
				.WithAccountPrivateKey(PrivateKey1)
				.Build();
	
			var result = UnitUnderTest.Download(param);
	
		    Assert.IsNotNull(result);
		    Assert.AreEqual(result.TransactionHash, transactionHash);
		    Assert.IsNotNull(result.Data);
		    Assert.AreEqual(result.Data.GetByteStream().GetContentAsString(), TestString);
		    Assert.AreEqual(result.Data.ContentType, "text/plain");
		    Assert.IsNotNull(result.Data.DataHash);
		    Assert.AreEqual(result.Data.Description, "string description");
		    Assert.AreEqual(result.Data.Name, "string name");
		    Assert.IsNotNull(result.Data.Metadata);
		    Assert.AreEqual(result.Data.Metadata.Count, 1);
		    Assert.IsFalse(result.Data.Metadata.Except(new Dictionary<string, string> {{"keystring", "valstring"}}).Any());
		    Assert.IsNotNull(result.Data.Timestamp);
		}
	
	    [TestMethod, Timeout(10000)]
		public void ShouldDownloadWithSecureMessageAsRecipient() {
			var transactionHash = TestDataRepository
					.GetData("UploaderSecureMessageIntegrationTests.ShouldUploadWithUseBlockchainSecureMessageAndRecipientPublicKey", "transactionHash");
			var param = DownloadParameter.Create(transactionHash)
				.WithAccountPrivateKey(PrivateKey2)
				.Build();
	
			var result = UnitUnderTest.Download(param);
	
		    Assert.IsNotNull(result);
		    Assert.AreEqual(result.TransactionHash, transactionHash);
		    Assert.IsNotNull(result.Data);
		    Assert.AreEqual(result.Data.GetByteStream().GetContentAsString(), TestString);
		    Assert.AreEqual(result.Data.ContentType, "text/plain");
		    Assert.IsNotNull(result.Data.DataHash);
		    Assert.AreEqual(result.Data.Description, "string description");
		    Assert.AreEqual(result.Data.Name, "string name");
		    Assert.IsNotNull(result.Data.Metadata);
		    Assert.AreEqual(result.Data.Metadata.Count, 1);
		    Assert.IsFalse(result.Data.Metadata.Except(new Dictionary<string, string> {{"keystring", "valstring"}}).Any());
		    Assert.IsNotNull(result.Data.Timestamp);
		}
	
	    [TestMethod, Timeout(10000), ExpectedException(typeof(DownloadFailureException))]
	    public void FailDownloadWithWrongPrivateKey() {
		    var transactionHash = TestDataRepository
			    .GetData("UploaderSecureMessageIntegrationTests.ShouldUploadWithUseBlockchainSecureMessageAndRecipientPublicKey", "transactionHash");
		    var param = DownloadParameter.Create(transactionHash)
			    .WithAccountPrivateKey("7FE209FAA5DE3E1EDEBD169091145788FF7B0847AD2FE04FB7706A660BFCAF0A")
			    .Build();

		    UnitUnderTest.Download(param);
	    }

	    [TestMethod, Timeout(10000), ExpectedException(typeof(DownloadFailureException))]
	    public void FailDownloadWithNoPrivateKey() {
		    var transactionHash = TestDataRepository
			    .GetData("UploaderSecureMessageIntegrationTests.ShouldUploadWithUseBlockchainSecureMessageAndRecipientPublicKey", "transactionHash");
		    var param = DownloadParameter.Create(transactionHash)
			    .Build();

		    UnitUnderTest.Download(param);
	    }

   }
}