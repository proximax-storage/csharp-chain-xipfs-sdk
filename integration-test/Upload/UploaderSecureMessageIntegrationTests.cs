using System.Collections.Generic;
using System.Text;
using IO.Proximax.SDK.Connections;
using IO.Proximax.SDK.Models;
using IO.Proximax.SDK.Upload;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static IO.Proximax.SDK.Models.Constants;
using static IntegrationTests.IntegrationTestConfig;
using static IntegrationTests.TestDataRepository;
using static IntegrationTests.TestSupport.Constants;

namespace IntegrationTests.Upload
{
	[TestClass]
	public class UploaderSecureMessageIntegrationTests
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
		public void ShouldUploadWithUseBlockchainSecureMessage()
		{
			var param = UploadParameter.CreateForStringUpload(
					StringParameterData.Create(TestString, Encoding.UTF8, "string description", "string name",
						"text/plain", new Dictionary<string, string> {{"keystring", "valstring"}}),
					AccountPrivateKey1)
				.WithUseBlockchainSecureMessage(true)
				.Build();

			var result = UnitUnderTest.Upload(param);

			Assert.IsNotNull(result);
			Assert.AreEqual(result.Version, SchemaVersion);

			LogAndSaveResult(result, GetType().Name + ".ShouldUploadWithUseBlockchainSecureMessage");
		}

		[TestMethod, Timeout(30000)]
		public void ShouldUploadWithUseBlockchainSecureMessageAndRecipientPublicKey()
		{
			var param = UploadParameter.CreateForStringUpload(
					StringParameterData.Create(TestString, Encoding.UTF8, "string description", "string name",
						"text/plain", new Dictionary<string, string> {{"keystring", "valstring"}}),
					AccountPrivateKey1)
				.WithRecipientPublicKey(AccountPublicKey2)
				.WithUseBlockchainSecureMessage(true)
				.Build();

			var result = UnitUnderTest.Upload(param);

			Assert.IsNotNull(result);
			Assert.AreEqual(result.Version, SchemaVersion);

			LogAndSaveResult(result, GetType().Name + ".ShouldUploadWithUseBlockchainSecureMessageAndRecipientPublicKey");
		}

		[TestMethod, Timeout(30000)]
		public void ShouldUploadWithUseBlockchainSecureMessageAndRecipientAddress()
		{
			var param = UploadParameter.CreateForStringUpload(
					StringParameterData.Create(TestString, Encoding.UTF8, "string description", "string name",
						"text/plain", new Dictionary<string, string> {{"keystring", "valstring"}}),
					AccountPrivateKey1)
				.WithRecipientAddress(AccountAddress2)
				.WithUseBlockchainSecureMessage(true)
				.Build();

			var result = UnitUnderTest.Upload(param);

			Assert.IsNotNull(result);
			Assert.AreEqual(result.Version, SchemaVersion);

			LogAndSaveResult(result, GetType().Name + ".ShouldUploadWithUseBlockchainSecureMessageAndRecipientAddress");
		}
	}
}