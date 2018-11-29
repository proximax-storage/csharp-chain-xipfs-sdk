using IO.Proximax.SDK.Connections;
using IO.Proximax.SDK.Models;
using IO.Proximax.SDK.Search;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static IntegrationTests.IntegrationTestConfig;

namespace IntegrationTests.Download
{
    [TestClass]
    public class SearcherFilterIntegrationTest
    {
        private Searcher UnitUnderTest { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            UnitUnderTest = new Searcher(
                ConnectionConfig.CreateWithLocalIpfsConnection(
                    new BlockchainNetworkConnection(BlockchainNetworkType.MijinTest, BlockchainApiHost, BlockchainApiPort, BlockchainApiProtocol),
                    new IpfsConnection(IpfsApiHost, IpfsApiPort, BlockchainApiProtocol))
            );
        }

	    [TestMethod, Timeout(60000)]
		public void ShouldSearchWithNameFilter() {
			var param = SearchParameter.CreateForAddress(AccountAddress1)
				.WithNameFilter("byte array")
				.Build();
	
			var result = UnitUnderTest.Search(param);
	
		    Assert.IsNotNull(result);
		    Assert.IsNotNull(result.Results);
		    Assert.AreEqual(result.Results.Count, 10);
		    Assert.IsTrue(result.Results.TrueForAll(r => r.MessagePayload.Data.Name.Contains("byte array")));
		}
	
	    [TestMethod, Timeout(60000)]
		public void ShouldSearchWithDescriptionFilter() {
			var param = SearchParameter.CreateForAddress(AccountAddress1)
				.WithDescriptionFilter("byte array description")
				.Build();
	
			var result = UnitUnderTest.Search(param);
	
		    Assert.IsNotNull(result);
		    Assert.IsNotNull(result.Results);
		    Assert.AreEqual(result.Results.Count, 10);
		    Assert.IsTrue(result.Results.TrueForAll(r => r.MessagePayload.Data.Description.Contains("byte array description")));
		}
	
	    [TestMethod, Timeout(60000)]
		public void ShouldSearchWithMetadataKeyFilter() {
			var param = SearchParameter.CreateForAddress(AccountAddress1)
				.WithMetadataKeyFilter("bytearraykey")
				.Build();
	
			var result = UnitUnderTest.Search(param);
	
		    Assert.IsNotNull(result);
		    Assert.IsNotNull(result.Results);
		    Assert.AreEqual(result.Results.Count, 10);
		    Assert.IsTrue(result.Results.TrueForAll(r => r.MessagePayload.Data.Metadata.ContainsKey("bytearraykey")));
		}
	
	    [TestMethod, Timeout(60000)]
		public void ShouldSearchWithMetadataKeyAndValueFilter() {
			var param = SearchParameter.CreateForAddress(AccountAddress1)
				.WithMetadataKeyFilter("bytearraykey")
				.WithMetadataValueFilter("bytearrayval")
				.Build();
	
			var result = UnitUnderTest.Search(param);
	
		    Assert.IsNotNull(result);
		    Assert.IsNotNull(result.Results);
		    Assert.AreEqual(result.Results.Count, 10);
		    Assert.IsTrue(result.Results.TrueForAll(r => "bytearrayval".Equals(r.MessagePayload.Data.Metadata["bytearraykey"])));
		}
	
	    [TestMethod, Timeout(60000)]
		public void ShouldSearchWithAllFilter() {
			var param = SearchParameter.CreateForAddress(AccountAddress1)
				.WithNameFilter("byte array")
				.WithDescriptionFilter("byte array description")
				.WithMetadataKeyFilter("bytearraykey")
				.WithMetadataValueFilter("bytearrayval")
				.Build();
	
			var result = UnitUnderTest.Search(param);
	
		    Assert.IsNotNull(result);
		    Assert.IsNotNull(result.Results);
		    Assert.AreEqual(result.Results.Count, 10);
		    Assert.IsTrue(result.Results.TrueForAll(r => r.MessagePayload.Data.Name.Contains("byte array")));
		    Assert.IsTrue(result.Results.TrueForAll(r => r.MessagePayload.Data.Description.Contains("byte array description")));
		    Assert.IsTrue(result.Results.TrueForAll(r => "bytearrayval".Equals(r.MessagePayload.Data.Metadata["bytearraykey"])));
		}
	
  }
}