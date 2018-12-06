using System;
using System.Threading;
using System.Threading.Tasks;
using Proximax.Storage.SDK.Async;
using Proximax.Storage.SDK.Connections;
using Proximax.Storage.SDK.Models;
using Proximax.Storage.SDK.Search;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static IntegrationTests.IntegrationTestConfig;

namespace IntegrationTests.Search
{
    [TestClass]
    public class SearcherAsyncIntegrationTests
    {
        private Searcher UnitUnderTest { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            UnitUnderTest = new Searcher(
                ConnectionConfig.CreateWithLocalIpfsConnection(
                    new BlockchainNetworkConnection(BlockchainNetworkType.MijinTest, BlockchainApiHost,
                        BlockchainApiPort, BlockchainApiProtocol),
                    new IpfsConnection(IpfsApiHost, IpfsApiPort, BlockchainApiProtocol))
            );
        }

        [TestMethod, Timeout(30000)]
        public void ShouldSearchAsynchronouslyWithoutCallback()
        {
            var param = SearchParameter.CreateForAddress(AccountAddress1).Build();

            var asyncTask = UnitUnderTest.SearchAsync(param);

            while (!asyncTask.IsDone())
            {
                Thread.Sleep(50);
            }

            Assert.IsTrue(asyncTask.IsDone());
        }

        [TestMethod, Timeout(30000)]
        public void ShouldSearchAsynchronouslyWithSuccessCallback()
        {
            var param = SearchParameter.CreateForAddress(AccountAddress1).Build();

            var taskCompletionSource = new TaskCompletionSource<SearchResult>();
            var asyncCallbacks = AsyncCallbacks<SearchResult>.Create<SearchResult>(
                searchResult => taskCompletionSource.SetResult(searchResult), null);

            UnitUnderTest.SearchAsync(param, asyncCallbacks);
            taskCompletionSource.Task.Wait(5000);

            var result = taskCompletionSource.Task.Result;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Results);
            Assert.AreEqual(result.Results.Count, 10);
        }

        [TestMethod, Timeout(10000)]
        public void ShouldSearchAsynchronouslyWithFailureCallback()
        {
            var param = SearchParameter.CreateForAddress("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA")
                .Build();
            var taskCompletionSource = new TaskCompletionSource<Exception>();
            var asyncCallbacks = AsyncCallbacks<SearchResult>.Create<SearchResult>(
                null, ex => taskCompletionSource.SetResult(ex));

            UnitUnderTest.SearchAsync(param, asyncCallbacks);
            taskCompletionSource.Task.Wait(5000);

            var exception = taskCompletionSource.Task.Result;

            Assert.IsInstanceOfType(exception, exception.GetType());
        }
    }
}