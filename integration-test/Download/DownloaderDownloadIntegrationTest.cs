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
    public class DownloaderDownloadIntegrationTest
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

	    [TestMethod, Timeout(10000), ExpectedException(typeof(DownloadFailureException))]
	    public void FailWhenInvalidTransactionHash() {
	        var param = DownloadParameter
	            .Create("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA")
	            .Build();

	        UnitUnderTest.Download(param);
	    }

	    [TestMethod, Timeout(10000)]
		public void ShouldDownloadWithVersion() {
			var transactionHash = TestDataRepository
					.GetData("UploaderIntegrationTests.ShouldReturnVersion", "transactionHash");
			var param = DownloadParameter.Create(transactionHash).Build();
	
			var result = UnitUnderTest.Download(param);
	
		    Assert.IsNotNull(result);
		    Assert.AreEqual(result.Version, SchemaVersion);
		}
	
	    [TestMethod, Timeout(10000)]
		public void ShouldDownloadByteArray() {
			var transactionHash = TestDataRepository
					.GetData("UploaderIntegrationTests.ShouldUploadByteArray", "transactionHash");
			var param = DownloadParameter.Create(transactionHash).Build();
	
			var result = UnitUnderTest.Download(param);
	
		    Assert.IsNotNull(result);
		    Assert.AreEqual(result.TransactionHash, transactionHash);
		    Assert.IsNotNull(result.Data);
		    Assert.AreEqual(new StreamReader(result.Data.GetByteStream()).ReadToEnd(), Encoding.UTF8.GetString(TestByteArray));
		    Assert.IsNull(result.Data.ContentType);
		    Assert.IsNotNull(result.Data.DataHash);
		    Assert.IsNull(result.Data.Description);
		    Assert.IsNull(result.Data.Name);
		    Assert.IsNotNull(result.Data.Metadata);
		    Assert.AreEqual(result.Data.Metadata.Count, 0);
		    Assert.IsNotNull(result.Data.Timestamp);
		}

	    [TestMethod, Timeout(10000)]
		public void ShouldDownloadByteArrayWithCompleteDetails() {
			var transactionHash = TestDataRepository
					.GetData("UploaderIntegrationTests.ShouldUploadByteArrayWithCompleteDetails", "transactionHash");
			var param = DownloadParameter.Create(transactionHash).Build();
	
			var result = UnitUnderTest.Download(param);
	
		    Assert.IsNotNull(result);
		    Assert.AreEqual(result.TransactionHash, transactionHash);
		    Assert.IsNotNull(result.Data);
		    Assert.AreEqual(new StreamReader(result.Data.GetByteStream()).ReadToEnd(), Encoding.UTF8.GetString(TestByteArray));
		    Assert.AreEqual(result.Data.ContentType, "text/plain");
		    Assert.IsNotNull(result.Data.DataHash);
		    Assert.AreEqual(result.Data.Description, "byte array description");
		    Assert.AreEqual(result.Data.Name, "byte array");
		    Assert.IsNotNull(result.Data.Metadata);
		    Assert.AreEqual(result.Data.Metadata.Count, 1);
		    Assert.IsFalse(result.Data.Metadata.Except(new Dictionary<string, string> {{"bytearraykey", "bytearrayval"}}).Any());
		    Assert.IsNotNull(result.Data.Timestamp);
		}

	    [TestMethod, Timeout(10000)]
	    public void ShouldDownloadFile() {
		    var transactionHash = TestDataRepository
			    .GetData("UploaderIntegrationTests.ShouldUploadFile", "transactionHash");
		    var param = DownloadParameter.Create(transactionHash).Build();
	
		    var result = UnitUnderTest.Download(param);
	
		    Assert.IsNotNull(result);
		    Assert.AreEqual(result.TransactionHash, transactionHash);
		    Assert.IsNotNull(result.Data);
		    Assert.AreEqual(new StreamReader(result.Data.GetByteStream()).ReadToEnd(), new StreamReader(new FileStream(TestTextFile, FileMode.Open, FileAccess.Read)).ReadToEnd());
		    Assert.IsNull(result.Data.ContentType);
		    Assert.IsNotNull(result.Data.DataHash);
		    Assert.IsNull(result.Data.Description);
		    Assert.AreEqual(result.Data.Name, "test_text_file.txt");
		    Assert.IsNotNull(result.Data.Metadata);
		    Assert.AreEqual(result.Data.Metadata.Count, 0);
		    Assert.IsNotNull(result.Data.Timestamp);
	    }

	    [TestMethod, Timeout(10000)]
	    public void ShouldDownloadFileWithCompleteDetails() {
		    var transactionHash = TestDataRepository
			    .GetData("UploaderIntegrationTests.ShouldUploadFileWithCompleteDetails", "transactionHash");
		    var param = DownloadParameter.Create(transactionHash).Build();
	
		    var result = UnitUnderTest.Download(param);
	
		    Assert.IsNotNull(result);
		    Assert.AreEqual(result.TransactionHash, transactionHash);
		    Assert.IsNotNull(result.Data);
		    Assert.AreEqual(new StreamReader(result.Data.GetByteStream()).ReadToEnd(), new StreamReader(new FileStream(TestTextFile, FileMode.Open, FileAccess.Read)).ReadToEnd());
		    Assert.AreEqual(result.Data.ContentType, "text/plain");
		    Assert.IsNotNull(result.Data.DataHash);
		    Assert.AreEqual(result.Data.Description, "file description");
		    Assert.AreEqual(result.Data.Name, "file name");
		    Assert.AreEqual(result.Data.Metadata.Count, 1);
		    Assert.IsFalse(result.Data.Metadata.Except(new Dictionary<string, string> {{"filekey", "filename"}}).Any());
		    Assert.IsNotNull(result.Data.Timestamp);
	    }

	    [TestMethod, Timeout(10000)]
	    public void ShouldDownloadUrlResource() {
		    var transactionHash = TestDataRepository
			    .GetData("UploaderIntegrationTests.ShouldUploadUrlResource", "transactionHash");
		    var param = DownloadParameter.Create(transactionHash).Build();
	
		    var result = UnitUnderTest.Download(param);
	
		    Assert.IsNotNull(result);
		    Assert.AreEqual(result.TransactionHash, transactionHash);
		    Assert.IsNotNull(result.Data);
		    Assert.AreEqual(new StreamReader(result.Data.GetByteStream()).ReadToEnd(), new StreamReader(new FileStream(TestImagePngFile, FileMode.Open, FileAccess.Read)).ReadToEnd());
		    Assert.IsNull(result.Data.ContentType);
		    Assert.IsNotNull(result.Data.DataHash);
		    Assert.IsNull(result.Data.Description);
		    Assert.IsNull(result.Data.Name);
		    Assert.IsNotNull(result.Data.Metadata);
		    Assert.AreEqual(result.Data.Metadata.Count, 0);
		    Assert.IsNotNull(result.Data.Timestamp);
	    }

	    [TestMethod, Timeout(10000)]
	    public void ShouldDownloadUrlResourceWithCompleteDetails() {
		    var transactionHash = TestDataRepository
			    .GetData("UploaderIntegrationTests.ShouldUploadUrlResourceWithCompleteDetails", "transactionHash");
		    var param = DownloadParameter.Create(transactionHash).Build();
	
		    var result = UnitUnderTest.Download(param);
	
		    Assert.IsNotNull(result);
		    Assert.AreEqual(result.TransactionHash, transactionHash);
		    Assert.IsNotNull(result.Data);
		    Assert.AreEqual(new StreamReader(result.Data.GetByteStream()).ReadToEnd(), new StreamReader(new FileStream(TestImagePngFile, FileMode.Open, FileAccess.Read)).ReadToEnd());
		    Assert.AreEqual(result.Data.ContentType, "image/png");
		    Assert.IsNotNull(result.Data.DataHash);
		    Assert.AreEqual(result.Data.Description, "url description");
		    Assert.AreEqual(result.Data.Name, "url name");
		    Assert.AreEqual(result.Data.Metadata.Count, 1);
		    Assert.IsFalse(result.Data.Metadata.Except(new Dictionary<string, string> {{"urlkey", "urlval"}}).Any());
		    Assert.IsNotNull(result.Data.Timestamp);
	    }

	    [TestMethod, Timeout(10000)]
	    public void ShouldDownloadFilesAsZip() {
		    var transactionHash = TestDataRepository
			    .GetData("UploaderIntegrationTests.ShouldUploadFilesAsZip", "transactionHash");
		    var param = DownloadParameter.Create(transactionHash).Build();
	
		    var result = UnitUnderTest.Download(param);
	
		    Assert.IsNotNull(result);
		    Assert.AreEqual(result.TransactionHash, transactionHash);
		    Assert.IsNotNull(result.Data);
		    Assert.IsNotNull(new StreamReader(result.Data.GetByteStream()).ReadToEnd());
		    Assert.AreEqual(result.Data.ContentType, "application/zip");
		    Assert.IsNotNull(result.Data.DataHash);
		    Assert.IsNull(result.Data.Description);
		    Assert.IsNull(result.Data.Name);
		    Assert.AreEqual(result.Data.Metadata.Count, 0);
		    Assert.IsNotNull(result.Data.Timestamp);
	    }

	    [TestMethod, Timeout(10000)]
	    public void ShouldDownloadFilesAsZipWithCompleteDetails() {
		    var transactionHash = TestDataRepository
			    .GetData("UploaderIntegrationTests.ShouldUploadFilesAsZipWithCompleteDetails", "transactionHash");
		    var param = DownloadParameter.Create(transactionHash).Build();
	
		    var result = UnitUnderTest.Download(param);
	
		    Assert.IsNotNull(result);
		    Assert.AreEqual(result.TransactionHash, transactionHash);
		    Assert.IsNotNull(result.Data);
		    Assert.IsNotNull(new StreamReader(result.Data.GetByteStream()).ReadToEnd());
		    Assert.AreEqual(result.Data.ContentType, "application/zip");
		    Assert.IsNotNull(result.Data.DataHash);
		    Assert.AreEqual(result.Data.Description, "zip description");
		    Assert.AreEqual(result.Data.Name, "zip name");
		    Assert.AreEqual(result.Data.Metadata.Count, 1);
		    Assert.IsFalse(result.Data.Metadata.Except(new Dictionary<string, string> {{"zipkey", "zipvalue"}}).Any());
		    Assert.IsNotNull(result.Data.Timestamp);
	    }
	    
	    [TestMethod, Timeout(10000)]
	    public void ShouldDownloadString() {
		    var transactionHash = TestDataRepository
			    .GetData("UploaderIntegrationTests.ShouldUploadString", "transactionHash");
		    var param = DownloadParameter.Create(transactionHash).Build();
	
		    var result = UnitUnderTest.Download(param);
	
		    Assert.IsNotNull(result);
		    Assert.AreEqual(result.TransactionHash, transactionHash);
		    Assert.IsNotNull(result.Data);
		    Assert.AreEqual(new StreamReader(result.Data.GetByteStream()).ReadToEnd(), TestString);
		    Assert.IsNull(result.Data.ContentType);
		    Assert.IsNotNull(result.Data.DataHash);
		    Assert.IsNull(result.Data.Description);
		    Assert.IsNull(result.Data.Name);
		    Assert.AreEqual(result.Data.Metadata.Count, 0);
		    Assert.IsNotNull(result.Data.Timestamp);
	    }
	    
	    [TestMethod, Timeout(10000)]
	    public void ShouldDownloadStringWithCompleteDetails() {
		    var transactionHash = TestDataRepository
			    .GetData("UploaderIntegrationTests.ShouldUploadStringWithCompleteDetails", "transactionHash");
		    var param = DownloadParameter.Create(transactionHash).Build();
	
		    var result = UnitUnderTest.Download(param);
	
		    Assert.IsNotNull(result);
		    Assert.AreEqual(result.TransactionHash, transactionHash);
		    Assert.IsNotNull(result.Data);
		    Assert.AreEqual(new StreamReader(result.Data.GetByteStream()).ReadToEnd(), TestString);
		    Assert.AreEqual(result.Data.ContentType, "text/plain");
		    Assert.IsNotNull(result.Data.DataHash);
		    Assert.AreEqual(result.Data.Description, "string description");
		    Assert.AreEqual(result.Data.Name, "string name");
		    Assert.AreEqual(result.Data.Metadata.Count, 1);
		    Assert.IsFalse(result.Data.Metadata.Except(new Dictionary<string, string> {{"keystring", "valstring"}}).Any());
		    Assert.IsNotNull(result.Data.Timestamp);
	    }

	    [TestMethod, Timeout(10000), ExpectedException(typeof(NotSupportedException))]
	    public void FailDownloadOnGetByteStreamWhenContentTypeIsDirectory() {
		    var transactionHash = TestDataRepository
			    .GetData("UploaderIntegrationTests.ShouldUploadPath", "transactionHash");
		    var param = DownloadParameter.Create(transactionHash).Build();
	
		    var result = UnitUnderTest.Download(param);
		    result.Data.GetByteStream();
	    }
    }
}