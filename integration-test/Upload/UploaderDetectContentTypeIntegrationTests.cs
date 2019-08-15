using ProximaX.Sirius.Storage.SDK.Connections;
using ProximaX.Sirius.Storage.SDK.Models;
using ProximaX.Sirius.Storage.SDK.Upload;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static IntegrationTests.IntegrationTestConfig;

// TODO
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
                ConnectionConfig.CreateWithLocalIpfsConnection(
                    new BlockchainNetworkConnection(BlockchainNetworkType.MijinTest, BlockchainApiHost,
                        BlockchainApiPort, BlockchainApiProtocol),
                    new IpfsConnection(IpfsApiHost, IpfsApiPort, BlockchainApiProtocol))
            );
        }

//		[TestMethod, Timeout(30000)]
//		public void ShouldUploadWithEnabledDetectContentType() {
//			var param = UploadParameter
//				.CreateForFileUpload(TestPdfFile2, AccountPrivateKey1)
//				.WithDetectContentType(true)
//				.Build();
//
//			var result = UnitUnderTest.Upload(param);
//
//			Assert.IsNotNull(result);
//			Assert.IsNotNull(result.Data.ContentType);
//			
//			LogAndSaveResult(result, GetType().Name + ".shouldUploadWithEnabledDetectContentType");
//		}
//
//		[TestMethod, Timeout(30000)]
//		public void ShouldUploadWithDisabledDetectContentType() {
//			var param = UploadParameter
//				.CreateForFileUpload(TestPdfFile2, AccountPrivateKey1)
//				.WithDetectContentType(false)
//				.Build();
//
//			var result = UnitUnderTest.Upload(param);
//
//			Assert.IsNotNull(result);
//			Assert.IsNull(result.Data.ContentType);
//			
//			LogAndSaveResult(result, GetType().Name + ".shouldUploadWithDisabledDetectContentType");
//		}
    }
}