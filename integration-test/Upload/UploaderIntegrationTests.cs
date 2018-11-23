using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IO.Proximax.SDK.Connections;
using IO.Proximax.SDK.Models;
using IO.Proximax.SDK.Upload;
using IO.Proximax.SDK.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static IO.Proximax.SDK.Models.Constants;
using static IntegrationTests.IntegrationTestConfig;
using static IntegrationTests.TestDataRepository;
using static IntegrationTests.TestSupport.Constants;
using static IntegrationTests.TestSupport.FileHelper;

namespace IntegrationTests.Upload
{
	[TestClass]
	public class UploaderIntegrationTests
	{
		private Uploader UnitUnderTest { get; set; }

		[TestInitialize]
		public void TestInitialize()
		{
			UnitUnderTest = new Uploader(
				ConnectionConfig.CreateWithLocalIpfsConnection(
					new BlockchainNetworkConnection(BlockchainNetworkType.MijinTest, BlockchainApiHost, BlockchainApiPort, BlockchainApiProtocol),
					new IpfsConnection(IpfsApiHost, IpfsApiPort, BlockchainApiProtocol))
			);
		}

		[TestMethod, Timeout(10000)]
		public void ShouldReturnVersion()
		{
			var param = UploadParameter.CreateForStringUpload(
					TestString, PrivateKey1)
				.Build();

			var result = UnitUnderTest.Upload(param);

			Assert.IsNotNull(result);
			Assert.AreEqual(result.Version, SchemaVersion);

			LogAndSaveResult(result, GetType().Name + ".ShouldReturnVersion");
		}

		[TestMethod, Timeout(10000)]
		public void ShouldUploadByteArray()
		{
			var param = UploadParameter.CreateForByteArrayUpload(
					TestByteArray, PrivateKey1)
				.Build();

			var result = UnitUnderTest.Upload(param);

			Assert.IsNotNull(result);
			Assert.IsNotNull(result.TransactionHash);
			Assert.IsNull(result.Data.ContentType);
			Assert.IsNotNull(result.Data.DataHash);
			Assert.IsNull(result.Data.Description);
			Assert.IsNull(result.Data.Name);
			Assert.IsNull(result.Data.Metadata);
			Assert.IsNotNull(result.Data.Timestamp);

			LogAndSaveResult(result, GetType().Name + ".ShouldUploadByteArray");
		}

		[TestMethod, Timeout(10000)]
		public void ShouldUploadByteArrayWithCompleteDetails()
		{
			var param = UploadParameter.CreateForByteArrayUpload(
					ByteArrayParameterData.Create(TestByteArray, "byte array description", "byte array",
						"text/plain", new Dictionary<string, string> {{"bytearraykey", "bytearrayval"}}),
					PrivateKey1)
				.Build();

			var result = UnitUnderTest.Upload(param);

			Assert.IsNotNull(result);
			Assert.IsNotNull(result.TransactionHash);
			Assert.AreEqual(result.Data.ContentType, "text/plain");
			Assert.IsNotNull(result.Data.DataHash);
			Assert.AreEqual(result.Data.Description, "byte array description");
			Assert.AreEqual(result.Data.Name, "byte array");
			Assert.AreEqual(result.Data.Metadata.Count, 1);
			Assert.IsFalse(result.Data.Metadata.Except(new Dictionary<string, string> {{"bytearraykey", "bytearrayval"}}).Any());
			Assert.IsNotNull(result.Data.Timestamp);

			LogAndSaveResult(result, GetType().Name + ".ShouldUploadByteArrayWithCompleteDetails");
		}

		[TestMethod, Timeout(10000)]
		public void ShouldUploadFile()
		{
			var param = UploadParameter.CreateForFileUpload(TestTextFile, PrivateKey1)
				.Build();

			var result = UnitUnderTest.Upload(param);

			Assert.IsNotNull(result);
			Assert.IsNotNull(result.TransactionHash);
			Assert.IsNull(result.Data.ContentType);
			Assert.IsNotNull(result.Data.DataHash);
			Assert.IsNull(result.Data.Description);
			Assert.AreEqual(result.Data.Name, "test_text_file.txt");
			Assert.IsNull(result.Data.Metadata);
			Assert.IsNotNull(result.Data.Timestamp);

			LogAndSaveResult(result, GetType().Name + ".ShouldUploadFile");
		}

		[TestMethod, Timeout(10000)]
		public void ShouldUploadFileWithCompleteDetails()
		{
			var param = UploadParameter.CreateForFileUpload(
					FileParameterData.Create(TestTextFile, "file description", "file name",
						"text/plain", new Dictionary<string, string> {{"filekey", "filename"}}),
					PrivateKey1)
				.Build();

			var result = UnitUnderTest.Upload(param);

			Assert.IsNotNull(result);
			Assert.IsNotNull(result.TransactionHash);
			Assert.AreEqual(result.Data.ContentType, "text/plain");
			Assert.IsNotNull(result.Data.DataHash);
			Assert.AreEqual(result.Data.Description, "file description");
			Assert.AreEqual(result.Data.Name, "file name");
			Assert.AreEqual(result.Data.Metadata.Count, 1);
			Assert.IsFalse(result.Data.Metadata.Except(new Dictionary<string, string> {{"filekey", "filename"}}).Any());
			Assert.IsNotNull(result.Data.Timestamp);

			LogAndSaveResult(result, GetType().Name + ".ShouldUploadFileWithCompleteDetails");
		}

		[TestMethod, Timeout(30000)]
		public void ShouldUploadUrlResource()
		{
			var param = UploadParameter.CreateForUrlResourceUpload(
					FileUrlFromRelativePath(TestImagePngFile), PrivateKey1)
				.Build();

			var result = UnitUnderTest.Upload(param);

			Assert.IsNotNull(result);
			Assert.IsNotNull(result.TransactionHash);
			Assert.IsNull(result.Data.ContentType);
			Assert.IsNotNull(result.Data.DataHash);
			Assert.IsNull(result.Data.Description);
			Assert.IsNull(result.Data.Name);
			Assert.IsNull(result.Data.Metadata);
			Assert.IsNotNull(result.Data.Timestamp);

			LogAndSaveResult(result, GetType().Name + ".ShouldUploadUrlResource");
		}

		[TestMethod, Timeout(30000)]
		public void ShouldUploadUrlResourceWithCompleteDetails()
		{
			var param = UploadParameter.CreateForUrlResourceUpload(
					UrlResourceParameterData.Create(FileUrlFromRelativePath(TestImagePngFile), "url description",
						"url name", "image/png", new Dictionary<string, string> {{"urlkey", "urlval"}}),
					PrivateKey1)
				.Build();

			var result = UnitUnderTest.Upload(param);

			Assert.IsNotNull(result.TransactionHash);
			Assert.AreEqual(result.Data.ContentType, "image/png");
			Assert.IsNotNull(result.Data.DataHash);
			Assert.AreEqual(result.Data.Description, "url description");
			Assert.AreEqual(result.Data.Name, "url name");
			Assert.AreEqual(result.Data.Metadata.Count, 1);
			Assert.IsFalse(result.Data.Metadata.Except(new Dictionary<string, string> {{"urlkey", "urlval"}}).Any());
			Assert.IsNotNull(result.Data.Timestamp);

			LogAndSaveResult(result, GetType().Name + ".ShouldUploadUrlResourceWithCompleteDetails");
		}

		[TestMethod, Timeout(10000)]
		public void ShouldUploadFilesAsZip()
		{
			var param = UploadParameter.CreateForFilesAsZipUpload(
					new List<string> {TestTextFile, TestHtmlFile}, PrivateKey1)
				.Build();

			var result = UnitUnderTest.Upload(param);

			Assert.IsNotNull(result);
			Assert.IsNotNull(result.TransactionHash);
			Assert.AreEqual(result.Data.ContentType, "application/zip");
			Assert.IsNotNull(result.Data.DataHash);
			Assert.IsNull(result.Data.Description);
			Assert.IsNull(result.Data.Name);
			Assert.IsNull(result.Data.Metadata);
			Assert.IsNotNull(result.Data.Timestamp);

			LogAndSaveResult(result, GetType().Name + ".ShouldUploadFilesAsZip");
		}

		[TestMethod, Timeout(10000)]
		public void ShouldUploadFilesAsZipWithCompleteDetails()
		{
			var param = UploadParameter.CreateForFilesAsZipUpload(
					FilesAsZipParameterData.Create(new List<string> {TestTextFile, TestHtmlFile}, "zip description",
						"zip name", new Dictionary<string, string> {{"zipkey", "zipvalue"}}),
					PrivateKey1)
				.Build();

			var result = UnitUnderTest.Upload(param);

			Assert.IsNotNull(result.TransactionHash);
			Assert.AreEqual(result.Data.ContentType, "application/zip");
			Assert.IsNotNull(result.Data.DataHash);
			Assert.AreEqual(result.Data.Description, "zip description");
			Assert.AreEqual(result.Data.Name, "zip name");
			Assert.AreEqual(result.Data.Metadata.Count, 1);
			Assert.IsFalse(result.Data.Metadata.Except(new Dictionary<string, string> {{"zipkey", "zipvalue"}}).Any());
			Assert.IsNotNull(result.Data.Timestamp);

			LogAndSaveResult(result, GetType().Name + ".ShouldUploadFilesAsZipWithCompleteDetails");
		}

		[TestMethod, Timeout(10000)]
		public void ShouldUploadString()
		{
			var param = UploadParameter.CreateForStringUpload(TestString, PrivateKey1)
				.Build();

			var result = UnitUnderTest.Upload(param);

			Assert.IsNotNull(result);
			Assert.IsNotNull(result.TransactionHash);
			Assert.IsNull(result.Data.ContentType);
			Assert.IsNotNull(result.Data.DataHash);
			Assert.IsNull(result.Data.Description);
			Assert.IsNull(result.Data.Name);
			Assert.IsNull(result.Data.Metadata);
			Assert.IsNotNull(result.Data.Timestamp);

			LogAndSaveResult(result, GetType().Name + ".ShouldUploadString");
		}

		[TestMethod, Timeout(10000)]
		public void ShouldUploadStringWithCompleteDetails()
		{
			var param = UploadParameter.CreateForStringUpload(
					StringParameterData.Create(TestString, Encoding.UTF8, "string description", "string name",
						"text/plain", new Dictionary<string, string> {{"keystring", "valstring"}}),
					PrivateKey1)
				.Build();

			var result = UnitUnderTest.Upload(param);

			Assert.IsNotNull(result.TransactionHash);
			Assert.AreEqual(result.Data.ContentType, "text/plain");
			Assert.IsNotNull(result.Data.DataHash);
			Assert.AreEqual(result.Data.Description, "string description");
			Assert.AreEqual(result.Data.Name, "string name");
			Assert.AreEqual(result.Data.Metadata.Count, 1);
			Assert.IsFalse(result.Data.Metadata.Except(new Dictionary<string, string> {{"keystring", "valstring"}}).Any());
			Assert.IsNotNull(result.Data.Timestamp);

			LogAndSaveResult(result, GetType().Name + ".ShouldUploadStringWithCompleteDetails");
		}

		[TestMethod, Timeout(10000)]
		public void ShouldUploadPath()
		{
			var param = UploadParameter.CreateForPathUpload(TestPathFile, PrivateKey1)
				.Build();

			var result = UnitUnderTest.Upload(param);

			Assert.IsNotNull(result);
			Assert.IsNotNull(result.TransactionHash);
			Assert.AreEqual(result.Data.ContentType, PathUploadContentType);
			Assert.IsNotNull(result.Data.DataHash);
			Assert.IsNull(result.Data.Description);
			Assert.IsNull(result.Data.Name);
			Assert.IsNull(result.Data.Metadata);
			Assert.IsNotNull(result.Data.Timestamp);

			LogAndSaveResult(result, GetType().Name + ".ShouldUploadPath");
		}

		[TestMethod, Timeout(10000)]
		public void ShouldUploadPathWithCompleteDetails()
		{
			var param = UploadParameter.CreateForPathUpload(
					PathParameterData.Create(TestPathFile, "path description", "path name",
						new Dictionary<string, string> {{"pathkey", "pathval"}}),
					PrivateKey1)
				.Build();

			var result = UnitUnderTest.Upload(param);

			Assert.IsNotNull(result.TransactionHash);
			Assert.AreEqual(result.Data.ContentType, PathUploadContentType);
			Assert.IsNotNull(result.Data.DataHash);
			Assert.AreEqual(result.Data.Description, "path description");
			Assert.AreEqual(result.Data.Name, "path name");
			Assert.AreEqual(result.Data.Metadata.Count, 1);
			Assert.IsFalse(result.Data.Metadata.Except(new Dictionary<string, string> {{"pathkey", "pathval"}}).Any());
			Assert.IsNotNull(result.Data.Timestamp);

			LogAndSaveResult(result, GetType().Name + ".ShouldUploadPathWithCompleteDetails");
		}
	}
}