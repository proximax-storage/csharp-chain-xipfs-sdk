using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Proximax.Storage.SDK.Connections;
using Proximax.Storage.SDK.Download;
using Proximax.Storage.SDK.Exceptions;
using Proximax.Storage.SDK.Models;
using Proximax.Storage.SDK.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static IntegrationTests.IntegrationTestConfig;
using static Proximax.Storage.SDK.Models.Constants;
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
                    new BlockchainNetworkConnection(BlockchainNetworkType.MijinTest, BlockchainApiHost,
                        BlockchainApiPort, BlockchainApiProtocol),
                    new IpfsConnection(IpfsApiHost, IpfsApiPort, BlockchainApiProtocol))
            );
        }

        [TestMethod, Timeout(10000), ExpectedException(typeof(DownloadFailureException))]
        public void FailWhenInvalidTransactionHash()
        {
            var param = DownloadParameter
                .Create("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA")
                .Build();

            UnitUnderTest.Download(param);
        }

        [TestMethod, Timeout(10000)]
        public void ShouldDownloadWithVersion()
        {
            var transactionHash = TestDataRepository
                .GetData("UploaderIntegrationTests.ShouldReturnVersion", "transactionHash");
            var param = DownloadParameter.Create(transactionHash).Build();

            var result = UnitUnderTest.Download(param);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Version, SchemaVersion);
        }

        [TestMethod, Timeout(10000)]
        public void ShouldDownloadByteArray()
        {
            var transactionHash = TestDataRepository
                .GetData("UploaderIntegrationTests.ShouldUploadByteArray", "transactionHash");
            var param = DownloadParameter.Create(transactionHash).Build();

            var result = UnitUnderTest.Download(param);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.TransactionHash, transactionHash);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(result.Data.GetByteStream().GetContentAsString(), Encoding.UTF8.GetString(TestByteArray));
            Assert.IsNull(result.Data.ContentType);
            Assert.IsNotNull(result.Data.DataHash);
            Assert.IsNull(result.Data.Description);
            Assert.IsNull(result.Data.Name);
            Assert.IsNotNull(result.Data.Metadata);
            Assert.AreEqual(result.Data.Metadata.Count, 0);
            Assert.IsNotNull(result.Data.Timestamp);
        }

        [TestMethod, Timeout(10000)]
        public void ShouldDownloadByteArrayWithCompleteDetails()
        {
            var transactionHash = TestDataRepository
                .GetData("UploaderIntegrationTests.ShouldUploadByteArrayWithCompleteDetails", "transactionHash");
            var param = DownloadParameter.Create(transactionHash).Build();

            var result = UnitUnderTest.Download(param);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.TransactionHash, transactionHash);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(result.Data.GetByteStream().GetContentAsString(), Encoding.UTF8.GetString(TestByteArray));
            Assert.AreEqual(result.Data.ContentType, "text/plain");
            Assert.IsNotNull(result.Data.DataHash);
            Assert.AreEqual(result.Data.Description, "byte array description");
            Assert.AreEqual(result.Data.Name, "byte array");
            Assert.IsNotNull(result.Data.Metadata);
            Assert.AreEqual(result.Data.Metadata.Count, 1);
            Assert.IsFalse(result.Data.Metadata
                .Except(new Dictionary<string, string> {{"bytearraykey", "bytearrayval"}}).Any());
            Assert.IsNotNull(result.Data.Timestamp);
        }

        [TestMethod, Timeout(10000)]
        public void ShouldDownloadFile()
        {
            var transactionHash = TestDataRepository
                .GetData("UploaderIntegrationTests.ShouldUploadFile", "transactionHash");
            var param = DownloadParameter.Create(transactionHash).Build();

            var result = UnitUnderTest.Download(param);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.TransactionHash, transactionHash);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(result.Data.GetByteStream().GetContentAsString(),
                new FileStream(TestTextFile, FileMode.Open, FileAccess.Read).GetContentAsString());
            Assert.IsNull(result.Data.ContentType);
            Assert.IsNotNull(result.Data.DataHash);
            Assert.IsNull(result.Data.Description);
            Assert.AreEqual(result.Data.Name, "test_text_file.txt");
            Assert.IsNotNull(result.Data.Metadata);
            Assert.AreEqual(result.Data.Metadata.Count, 0);
            Assert.IsNotNull(result.Data.Timestamp);
        }

        [TestMethod, Timeout(10000)]
        public void ShouldDownloadFileWithCompleteDetails()
        {
            var transactionHash = TestDataRepository
                .GetData("UploaderIntegrationTests.ShouldUploadFileWithCompleteDetails", "transactionHash");
            var param = DownloadParameter.Create(transactionHash).Build();

            var result = UnitUnderTest.Download(param);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.TransactionHash, transactionHash);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(result.Data.GetByteStream().GetContentAsString(),
                new FileStream(TestTextFile, FileMode.Open, FileAccess.Read).GetContentAsString());
            Assert.AreEqual(result.Data.ContentType, "text/plain");
            Assert.IsNotNull(result.Data.DataHash);
            Assert.AreEqual(result.Data.Description, "file description");
            Assert.AreEqual(result.Data.Name, "file name");
            Assert.AreEqual(result.Data.Metadata.Count, 1);
            Assert.IsFalse(result.Data.Metadata.Except(new Dictionary<string, string> {{"filekey", "filename"}}).Any());
            Assert.IsNotNull(result.Data.Timestamp);
        }

        [TestMethod, Timeout(10000)]
        public void ShouldDownloadUrlResource()
        {
            var transactionHash = TestDataRepository
                .GetData("UploaderIntegrationTests.ShouldUploadUrlResource", "transactionHash");
            var param = DownloadParameter.Create(transactionHash).Build();

            var result = UnitUnderTest.Download(param);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.TransactionHash, transactionHash);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(result.Data.GetByteStream().GetContentAsString(),
                new FileStream(TestImagePngFile, FileMode.Open, FileAccess.Read).GetContentAsString());
            Assert.IsNull(result.Data.ContentType);
            Assert.IsNotNull(result.Data.DataHash);
            Assert.IsNull(result.Data.Description);
            Assert.IsNull(result.Data.Name);
            Assert.IsNotNull(result.Data.Metadata);
            Assert.AreEqual(result.Data.Metadata.Count, 0);
            Assert.IsNotNull(result.Data.Timestamp);
        }

        [TestMethod, Timeout(10000)]
        public void ShouldDownloadUrlResourceWithCompleteDetails()
        {
            var transactionHash = TestDataRepository
                .GetData("UploaderIntegrationTests.ShouldUploadUrlResourceWithCompleteDetails", "transactionHash");
            var param = DownloadParameter.Create(transactionHash).Build();

            var result = UnitUnderTest.Download(param);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.TransactionHash, transactionHash);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(result.Data.GetByteStream().GetContentAsString(),
                new FileStream(TestImagePngFile, FileMode.Open, FileAccess.Read).GetContentAsString());
            Assert.AreEqual(result.Data.ContentType, "image/png");
            Assert.IsNotNull(result.Data.DataHash);
            Assert.AreEqual(result.Data.Description, "url description");
            Assert.AreEqual(result.Data.Name, "url name");
            Assert.AreEqual(result.Data.Metadata.Count, 1);
            Assert.IsFalse(result.Data.Metadata.Except(new Dictionary<string, string> {{"urlkey", "urlval"}}).Any());
            Assert.IsNotNull(result.Data.Timestamp);
        }

        [TestMethod, Timeout(10000)]
        public void ShouldDownloadStream()
        {
            var transactionHash = TestDataRepository
                .GetData("UploaderIntegrationTests.ShouldUploadStream", "transactionHash");
            var param = DownloadParameter.Create(transactionHash).Build();

            var result = UnitUnderTest.Download(param);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.TransactionHash, transactionHash);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(result.Data.GetByteStream().GetContentAsString(),
                new FileStream(TestTextFile, FileMode.Open, FileAccess.Read).GetContentAsString());
            Assert.IsNull(result.Data.ContentType);
            Assert.IsNotNull(result.Data.DataHash);
            Assert.IsNull(result.Data.Description);
            Assert.IsNull(result.Data.Name);
            Assert.IsNotNull(result.Data.Metadata);
            Assert.AreEqual(result.Data.Metadata.Count, 0);
            Assert.IsNotNull(result.Data.Timestamp);
        }

        [TestMethod, Timeout(10000)]
        public void ShouldDownloadStreamWithCompleteDetails()
        {
            var transactionHash = TestDataRepository
                .GetData("UploaderIntegrationTests.ShouldUploadStreamWithCompleteDetails", "transactionHash");
            var param = DownloadParameter.Create(transactionHash).Build();

            var result = UnitUnderTest.Download(param);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.TransactionHash, transactionHash);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(result.Data.GetByteStream().GetContentAsString(),
                new FileStream(TestTextFile, FileMode.Open, FileAccess.Read).GetContentAsString());
            Assert.AreEqual(result.Data.ContentType, "text/plain");
            Assert.IsNotNull(result.Data.DataHash);
            Assert.AreEqual(result.Data.Description, "stream description");
            Assert.AreEqual(result.Data.Name, "stream name");
            Assert.AreEqual(result.Data.Metadata.Count, 1);
            Assert.IsFalse(result.Data.Metadata.Except(new Dictionary<string, string> {{"streamkey", "streamval"}})
                .Any());
            Assert.IsNotNull(result.Data.Timestamp);
        }

        [TestMethod, Timeout(10000)]
        public void ShouldDownloadFilesAsZip()
        {
            var transactionHash = TestDataRepository
                .GetData("UploaderIntegrationTests.ShouldUploadFilesAsZip", "transactionHash");
            var param = DownloadParameter.Create(transactionHash).Build();

            var result = UnitUnderTest.Download(param);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.TransactionHash, transactionHash);
            Assert.IsNotNull(result.Data);
            Assert.IsNotNull(result.Data.GetByteStream().GetContentAsByteArray());
            Assert.AreEqual(result.Data.ContentType, "application/zip");
            Assert.IsNotNull(result.Data.DataHash);
            Assert.IsNull(result.Data.Description);
            Assert.IsNull(result.Data.Name);
            Assert.AreEqual(result.Data.Metadata.Count, 0);
            Assert.IsNotNull(result.Data.Timestamp);
        }

        [TestMethod, Timeout(10000)]
        public void ShouldDownloadFilesAsZipWithCompleteDetails()
        {
            var transactionHash = TestDataRepository
                .GetData("UploaderIntegrationTests.ShouldUploadFilesAsZipWithCompleteDetails", "transactionHash");
            var param = DownloadParameter.Create(transactionHash).Build();

            var result = UnitUnderTest.Download(param);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.TransactionHash, transactionHash);
            Assert.IsNotNull(result.Data);
            Assert.IsNotNull(result.Data.GetByteStream().GetContentAsByteArray());
            Assert.AreEqual(result.Data.ContentType, "application/zip");
            Assert.IsNotNull(result.Data.DataHash);
            Assert.AreEqual(result.Data.Description, "zip description");
            Assert.AreEqual(result.Data.Name, "zip name");
            Assert.AreEqual(result.Data.Metadata.Count, 1);
            Assert.IsFalse(result.Data.Metadata.Except(new Dictionary<string, string> {{"zipkey", "zipvalue"}}).Any());
            Assert.IsNotNull(result.Data.Timestamp);
        }

        [TestMethod, Timeout(10000)]
        public void ShouldDownloadString()
        {
            var transactionHash = TestDataRepository
                .GetData("UploaderIntegrationTests.ShouldUploadString", "transactionHash");
            var param = DownloadParameter.Create(transactionHash).Build();

            var result = UnitUnderTest.Download(param);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.TransactionHash, transactionHash);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(result.Data.GetByteStream().GetContentAsString(), TestString);
            Assert.IsNull(result.Data.ContentType);
            Assert.IsNotNull(result.Data.DataHash);
            Assert.IsNull(result.Data.Description);
            Assert.IsNull(result.Data.Name);
            Assert.AreEqual(result.Data.Metadata.Count, 0);
            Assert.IsNotNull(result.Data.Timestamp);
        }

        [TestMethod, Timeout(10000)]
        public void ShouldDownloadStringWithCompleteDetails()
        {
            var transactionHash = TestDataRepository
                .GetData("UploaderIntegrationTests.ShouldUploadStringWithCompleteDetails", "transactionHash");
            var param = DownloadParameter.Create(transactionHash).Build();

            var result = UnitUnderTest.Download(param);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.TransactionHash, transactionHash);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(result.Data.GetByteStream().GetContentAsString(), TestString);
            Assert.AreEqual(result.Data.ContentType, "text/plain");
            Assert.IsNotNull(result.Data.DataHash);
            Assert.AreEqual(result.Data.Description, "string description");
            Assert.AreEqual(result.Data.Name, "string name");
            Assert.AreEqual(result.Data.Metadata.Count, 1);
            Assert.IsFalse(result.Data.Metadata.Except(new Dictionary<string, string> {{"keystring", "valstring"}})
                .Any());
            Assert.IsNotNull(result.Data.Timestamp);
        }

        [TestMethod, Timeout(10000), ExpectedException(typeof(NotSupportedException))]
        public void FailDownloadOnGetByteStreamWhenContentTypeIsDirectory()
        {
            var transactionHash = TestDataRepository
                .GetData("UploaderIntegrationTests.ShouldUploadPath", "transactionHash");
            var param = DownloadParameter.Create(transactionHash).Build();

            var result = UnitUnderTest.Download(param);
            result.Data.GetByteStream();
        }
    }
}