using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using IO.Proximax.SDK.Connections;
using IO.Proximax.SDK.Download;
using IO.Proximax.SDK.Exceptions;
using IO.Proximax.SDK.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static IntegrationTests.IntegrationTestConfig;
using static IO.Proximax.SDK.Models.Constants;
using static IntegrationTests.TestSupport.Constants;

namespace IntegrationTests.Download
{
    [TestClass]
    public class DownloaderDirectDownloadIntegrationTest
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

	    [TestMethod, Timeout(10000), ExpectedException(typeof(DirectDownloadFailureException))]
	    public void FailWhenInvalidTransactionHash() {
	        var param = DirectDownloadParameter
	            .CreateFromTransactionHash("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA")
	            .Build();

	        UnitUnderTest.DirectDownload(param);
	    }

	    [TestMethod, Timeout(10000)]
		public void ShouldDownloadByteArrayByTransactionHash() {
			var transactionHash = TestDataRepository
					.GetData("UploaderIntegrationTests.ShouldUploadByteArray", "transactionHash");
			var param = DirectDownloadParameter.CreateFromTransactionHash(transactionHash).Build();
	
			var result = UnitUnderTest.DirectDownload(param);
	
		    Assert.IsNotNull(result);
		    Assert.AreEqual(new StreamReader(result).ReadToEnd(), Encoding.UTF8.GetString(TestByteArray));
		}

	    [TestMethod, Timeout(10000)]
	    public void ShouldDownloadFileByTransactionHash() {
		    var transactionHash = TestDataRepository
			    .GetData("UploaderIntegrationTests.ShouldUploadFile", "transactionHash");
		    var param = DirectDownloadParameter.CreateFromTransactionHash(transactionHash).Build();
	
		    var result = UnitUnderTest.DirectDownload(param);
	
		    Assert.IsNotNull(result);
			Assert.AreEqual(new StreamReader(result).ReadToEnd(), new StreamReader(new FileStream(TestTextFile, FileMode.Open, FileAccess.Read)).ReadToEnd());
	    }

	    [TestMethod, Timeout(10000)]
	    public void ShouldDownloadUrlResourceByTransactionHash() {
		    var transactionHash = TestDataRepository
			    .GetData("UploaderIntegrationTests.ShouldUploadUrlResource", "transactionHash");
		    var param = DirectDownloadParameter.CreateFromTransactionHash(transactionHash).Build();
	
		    var result = UnitUnderTest.DirectDownload(param);
	
		    Assert.IsNotNull(result);
			Assert.AreEqual(new StreamReader(result).ReadToEnd(), new StreamReader(new FileStream(TestImagePngFile, FileMode.Open, FileAccess.Read)).ReadToEnd());
	    }

	    [TestMethod, Timeout(10000)]
	    public void ShouldDownloadStreamByTransactionHash() {
		    var transactionHash = TestDataRepository
			    .GetData("UploaderIntegrationTests.ShouldUploadStream", "transactionHash");
		    var param = DirectDownloadParameter.CreateFromTransactionHash(transactionHash).Build();
	
		    var result = UnitUnderTest.DirectDownload(param);
	
		    Assert.IsNotNull(result);
			Assert.AreEqual(new StreamReader(result).ReadToEnd(), new StreamReader(new FileStream(TestTextFile, FileMode.Open, FileAccess.Read)).ReadToEnd());
	    }

	    [TestMethod, Timeout(10000)]
	    public void ShouldDownloadFilesAsZipByTransactionHash() {
		    var transactionHash = TestDataRepository
			    .GetData("UploaderIntegrationTests.ShouldUploadFilesAsZip", "transactionHash");
		    var param = DirectDownloadParameter.CreateFromTransactionHash(transactionHash).Build();
	
		    var result = UnitUnderTest.DirectDownload(param);
	
		    Assert.IsNotNull(result);
		    Assert.IsNotNull(new StreamReader(result).ReadToEnd());
	    }

	    [TestMethod, Timeout(10000)]
	    public void ShouldDownloadStringByTransactionHash() {
		    var transactionHash = TestDataRepository
			    .GetData("UploaderIntegrationTests.ShouldUploadString", "transactionHash");
		    var param = DirectDownloadParameter.CreateFromTransactionHash(transactionHash).Build();
	
		    var result = UnitUnderTest.DirectDownload(param);
	
		    Assert.IsNotNull(result);
		    Assert.AreEqual(new StreamReader(result).ReadToEnd(), TestString);
	    }
	    
	    [TestMethod, Timeout(10000), ExpectedException(typeof(DirectDownloadFailureException))]
	    public void FailDownloadByTransactionHashWhenContentTypeIsDirectory() {
		    var transactionHash = TestDataRepository
			    .GetData("UploaderIntegrationTests.ShouldUploadPath", "transactionHash");
		    var param = DirectDownloadParameter.CreateFromTransactionHash(transactionHash).Build();
	
		    UnitUnderTest.DirectDownload(param);
	    }
	    
	    [TestMethod, Timeout(10000), ExpectedException(typeof(DirectDownloadFailureException))]
	    public void FailWhenInvalidDataHash() {
	        var param = DirectDownloadParameter
	            .CreateFromDataHash("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA")
	            .Build();

	        UnitUnderTest.DirectDownload(param);
	    }

	    [TestMethod, Timeout(10000)]
		public void ShouldDownloadByteArrayByDataHash() {
			var dataHash = TestDataRepository
					.GetData("UploaderIntegrationTests.ShouldUploadByteArray", "dataHash");
			var param = DirectDownloadParameter.CreateFromDataHash(dataHash).Build();
	
			var result = UnitUnderTest.DirectDownload(param);
	
		    Assert.IsNotNull(result);
		    Assert.AreEqual(new StreamReader(result).ReadToEnd(), Encoding.UTF8.GetString(TestByteArray));
		}

	    [TestMethod, Timeout(10000)]
	    public void ShouldDownloadFileByDataHash() {
		    var dataHash = TestDataRepository
			    .GetData("UploaderIntegrationTests.ShouldUploadFile", "dataHash");
		    var param = DirectDownloadParameter.CreateFromDataHash(dataHash).Build();
	
		    var result = UnitUnderTest.DirectDownload(param);
	
		    Assert.IsNotNull(result);
			Assert.AreEqual(new StreamReader(result).ReadToEnd(), new StreamReader(new FileStream(TestTextFile, FileMode.Open, FileAccess.Read)).ReadToEnd());
	    }

	    [TestMethod, Timeout(10000)]
	    public void ShouldDownloadUrlResourceByDataHash() {
		    var dataHash = TestDataRepository
			    .GetData("UploaderIntegrationTests.ShouldUploadUrlResource", "dataHash");
		    var param = DirectDownloadParameter.CreateFromDataHash(dataHash).Build();
	
		    var result = UnitUnderTest.DirectDownload(param);
	
		    Assert.IsNotNull(result);
			Assert.AreEqual(new StreamReader(result).ReadToEnd(), new StreamReader(new FileStream(TestImagePngFile, FileMode.Open, FileAccess.Read)).ReadToEnd());
	    }

	    [TestMethod, Timeout(10000)]
	    public void ShouldDownloadStreamByDataHash() {
		    var dataHash = TestDataRepository
			    .GetData("UploaderIntegrationTests.ShouldUploadStream", "dataHash");
		    var param = DirectDownloadParameter.CreateFromDataHash(dataHash).Build();
	
		    var result = UnitUnderTest.DirectDownload(param);
	
		    Assert.IsNotNull(result);
			Assert.AreEqual(new StreamReader(result).ReadToEnd(), new StreamReader(new FileStream(TestTextFile, FileMode.Open, FileAccess.Read)).ReadToEnd());
	    }

	    [TestMethod, Timeout(10000)]
	    public void ShouldDownloadFilesAsZipByDataHash() {
		    var dataHash = TestDataRepository
			    .GetData("UploaderIntegrationTests.ShouldUploadFilesAsZip", "dataHash");
		    var param = DirectDownloadParameter.CreateFromDataHash(dataHash).Build();
	
		    var result = UnitUnderTest.DirectDownload(param);
	
		    Assert.IsNotNull(result);
		    Assert.IsNotNull(new StreamReader(result).ReadToEnd());
	    }

	    [TestMethod, Timeout(10000)]
	    public void ShouldDownloadStringByDataHash() {
		    var dataHash = TestDataRepository
			    .GetData("UploaderIntegrationTests.ShouldUploadString", "dataHash");
		    var param = DirectDownloadParameter.CreateFromDataHash(dataHash).Build();
	
		    var result = UnitUnderTest.DirectDownload(param);
	
		    Assert.IsNotNull(result);
		    Assert.AreEqual(new StreamReader(result).ReadToEnd(), TestString);
	    }
	    
	    [TestMethod, Timeout(10000), ExpectedException(typeof(DirectDownloadFailureException))]
	    public void FailDownloadByDataHashWhenContentTypeIsDirectory() {
		    var dataHash = TestDataRepository
			    .GetData("UploaderIntegrationTests.ShouldUploadPath", "dataHash");
		    var param = DirectDownloadParameter.CreateFromDataHash(dataHash).Build();
	
		    UnitUnderTest.DirectDownload(param);
	    }
    }
}