using System;
using IO.Proximax.SDK.Connections;
using IO.Proximax.SDK.Models;
using IO.Proximax.SDK.Search;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static IntegrationTests.IntegrationTestConfig;

namespace IntegrationTests.Search
{
    [TestClass]
    public class SearcherResultSizeIntegrationTest
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

	    [TestMethod, Timeout(10000)]
		public void ShouldSearchWithNoSpecifiedResultSize() {
			var param = SearchParameter.CreateForAddress(AccountAddress1).Build();
	
			var result = UnitUnderTest.Search(param);
	
		    Assert.IsNotNull(result);
		    Assert.IsNotNull(result.Results);
		    Assert.AreEqual(result.Results.Count, 10);
		}
	
	    [TestMethod, Timeout(10000)]
		public void ShouldSearchWith1ResultSize() {
		    var param = SearchParameter.CreateForAddress(AccountAddress1)
			    .WithResultSize(1)
			    .Build();
	
			var result = UnitUnderTest.Search(param);
	
		    Assert.IsNotNull(result);
		    Assert.IsNotNull(result.Results);
		    Assert.AreEqual(result.Results.Count, 1);
		}
	
	    [TestMethod, Timeout(10000)]
		public void ShouldSearchWith20ResultSize() {
		    var param = SearchParameter.CreateForAddress(AccountAddress1)
			    .WithResultSize(20)
			    .Build();
	
			var result = UnitUnderTest.Search(param);
	
		    Assert.IsNotNull(result);
		    Assert.IsNotNull(result.Results);
		    Assert.AreEqual(result.Results.Count, 20);
		}
	
	    [TestMethod, Timeout(10000), ExpectedException(typeof(ArgumentException))]
		public void FailSearchWithMoreThan20ResultSize() {
		    SearchParameter.CreateForAddress(AccountAddress1)
			    .WithResultSize(21)
			    .Build();
		}
	
	    [TestMethod, Timeout(10000), ExpectedException(typeof(ArgumentException))]
		public void FailSearchWithLessThan1ResultSize() {
		    SearchParameter.CreateForAddress(AccountAddress1)
			    .WithResultSize(0)
			    .Build();
		}
	
   }
}