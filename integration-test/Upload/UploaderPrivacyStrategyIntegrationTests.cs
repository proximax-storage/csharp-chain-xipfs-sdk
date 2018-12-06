using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IO.Proximax.SDK.Connections;
using IO.Proximax.SDK.Models;
using IO.Proximax.SDK.PrivacyStrategies;
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

	public class MyPrivacyStrategy: ICustomPrivacyStrategy
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