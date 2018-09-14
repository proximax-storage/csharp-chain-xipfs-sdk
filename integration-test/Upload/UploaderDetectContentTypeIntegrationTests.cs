using IO.Proximax.SDK.Connections;
using IO.Proximax.SDK.Models;
using IO.Proximax.SDK.Upload;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static IntegrationTests.Resources.Config;
using static IntegrationTests.TestSupport.Constants;

namespace IntegrationTests.Upload
{
	[TestClass]
	public class UploaderDetectContentTypeIntegrationTests
	{
		private Uploader UnitUnderTest { get; set; }

		[TestInitialize]
		public void TestInitialize()
		{
			UnitUnderTest = new Uploader(
				ConnectionConfig.Create(
					new BlockchainNetworkConnection(BlockchainNetworkType.Mijin_Test, BlockchainRestApiUrl),
					new IpfsConnection(IpfsUrl))
			);
		}

		[TestMethod, Timeout(10000)]
		public void ShouldUploadWithEnabledDetectContentType() {
			var param = UploadParameter
				.CreateForFileUpload(TestPdfFile2, PrivateKey1)
				.WithDetectContentType(true)
				.Build();

			var result = UnitUnderTest.Upload(param);

			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Data.ContentType);
		}

		[TestMethod, Timeout(10000)]
		public void ShouldUploadWithDisabledDetectContentType() {
			var param = UploadParameter
				.CreateForFileUpload(TestPdfFile2, PrivateKey1)
				.WithDetectContentType(false)
				.Build();

			var result = UnitUnderTest.Upload(param);

			Assert.IsNotNull(result);
			Assert.IsNull(result.Data.ContentType);
		}
	}
}