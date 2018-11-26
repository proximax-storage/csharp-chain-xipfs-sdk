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
	public class UploaderPrivacyStrategyIntegrationTests
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
		public void ShouldUploadFileWithPlainPrivacyStrategy()
		{
			var param = UploadParameter.CreateForFileUpload(TestTextFile, PrivateKey1)
				.WithPlainPrivacy()
				.Build();

			var result = UnitUnderTest.Upload(param);

			Assert.IsNotNull(result);
			Assert.IsNotNull(result.TransactionHash);
			Assert.IsNotNull(result.Data.DataHash);
			Assert.AreEqual(result.PrivacyType, (int) PrivacyType.Plain);

			LogAndSaveResult(result, GetType().Name + ".ShouldUploadFileWithPlainPrivacyStrategy");
		}

		[TestMethod, Timeout(10000)]
		public void ShouldUploadFileWithSecuredWithNemKeysPrivacyStrategy()
		{
			var param = UploadParameter.CreateForFileUpload(TestTextFile, PrivateKey1)
				.WithNemKeysPrivacy(PrivateKey1, PublicKey2)
				.Build();

			var result = UnitUnderTest.Upload(param);

			Assert.IsNotNull(result);
			Assert.IsNotNull(result.TransactionHash);
			Assert.IsNotNull(result.Data.DataHash);
			Assert.AreEqual(result.PrivacyType, (int) PrivacyType.NemKeys);

			LogAndSaveResult(result, GetType().Name + ".ShouldUploadFileWithSecuredWithNemKeysPrivacyStrategy");
		}

		[TestMethod, Timeout(10000)]
		public void ShouldUploadFileWithSecuredWithPasswordPrivacyStrategy()
		{
			var param = UploadParameter.CreateForFileUpload(TestTextFile, PrivateKey1)
				.WithPasswordPrivacy(TestPassword)
				.Build();

			var result = UnitUnderTest.Upload(param);

			Assert.IsNotNull(result);
			Assert.IsNotNull(result.TransactionHash);
			Assert.IsNotNull(result.Data.DataHash);
			Assert.AreEqual(result.PrivacyType, (int) PrivacyType.Password);

			LogAndSaveResult(result, GetType().Name + ".ShouldUploadFileWithSecuredWithPasswordPrivacyStrategy");
		}

	}
}