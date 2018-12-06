using System;
using System.Threading;
using System.Threading.Tasks;
using Proximax.Storage.SDK.Async;
using Proximax.Storage.SDK.Connections;
using Proximax.Storage.SDK.Download;
using Proximax.Storage.SDK.Models;
using Proximax.Storage.SDK.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static IntegrationTests.IntegrationTestConfig;

namespace IntegrationTests.Upload
{
    [TestClass]
    public class DownloaderDownloadAsyncIntegrationTests
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

        [TestMethod, Timeout(30000)]
        public void ShouldDownloadAsynchronouslyWithoutCallback()
        {
            var transactionHash = TestDataRepository
                .GetData("UploaderIntegrationTests.ShouldUploadByteArray", "transactionHash");
            var param = DownloadParameter.Create(transactionHash).Build();

            var asyncTask = UnitUnderTest.DownloadAsync(param);

            while (!asyncTask.IsDone())
            {
                Thread.Sleep(50);
            }

            Assert.IsTrue(asyncTask.IsDone());
        }

        [TestMethod, Timeout(30000)]
        public void ShouldDownloadAsynchronouslyWithSuccessCallback()
        {
            var transactionHash = TestDataRepository
                .GetData("UploaderIntegrationTests.ShouldUploadByteArray", "transactionHash");
            var param = DownloadParameter.Create(transactionHash).Build();

            var taskCompletionSource = new TaskCompletionSource<DownloadResult>();
            var asyncCallbacks = AsyncCallbacks<DownloadResult>.Create<DownloadResult>(
                downloadResult => taskCompletionSource.SetResult(downloadResult), null);

            UnitUnderTest.DownloadAsync(param, asyncCallbacks);
            taskCompletionSource.Task.Wait(5000);

            var result = taskCompletionSource.Task.Result;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data.GetByteStream().GetContentAsByteArray());
        }

        [TestMethod, Timeout(10000)]
        public void ShouldDownloadAsynchronouslyWithFailureCallback()
        {
            var param = DownloadParameter.Create("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA").Build();
            var taskCompletionSource = new TaskCompletionSource<Exception>();
            var asyncCallbacks = AsyncCallbacks<DownloadResult>.Create<DownloadResult>(
                null, ex => taskCompletionSource.SetResult(ex));

            UnitUnderTest.DownloadAsync(param, asyncCallbacks);
            taskCompletionSource.Task.Wait(5000);

            var exception = taskCompletionSource.Task.Result;

            Assert.IsInstanceOfType(exception, exception.GetType());
        }

        [TestMethod, Timeout(30000)]
        public void ShouldDownloadMultipleRequestsAsynchronously()
        {
            var transactionHash = TestDataRepository
                .GetData("UploaderIntegrationTests.ShouldUploadByteArray", "transactionHash");
            var param = DownloadParameter.Create(transactionHash).Build();

            var taskCompletionSource1 = new TaskCompletionSource<DownloadResult>();
            var taskCompletionSource2 = new TaskCompletionSource<DownloadResult>();
            var taskCompletionSource3 = new TaskCompletionSource<DownloadResult>();
            var asyncCallbacks1 = AsyncCallbacks<DownloadResult>.Create<DownloadResult>(
                downloadResult => taskCompletionSource1.SetResult(downloadResult), null);
            var asyncCallbacks2 = AsyncCallbacks<DownloadResult>.Create<DownloadResult>(
                downloadResult => taskCompletionSource2.SetResult(downloadResult), null);
            var asyncCallbacks3 = AsyncCallbacks<DownloadResult>.Create<DownloadResult>(
                downloadResult => taskCompletionSource3.SetResult(downloadResult), null);

            UnitUnderTest.DownloadAsync(param, asyncCallbacks1);
            UnitUnderTest.DownloadAsync(param, asyncCallbacks2);
            UnitUnderTest.DownloadAsync(param, asyncCallbacks3);
            taskCompletionSource1.Task.Wait(5000);
            taskCompletionSource2.Task.Wait(5000);
            taskCompletionSource3.Task.Wait(5000);

            var result1 = taskCompletionSource1.Task.Result;
            Assert.IsNotNull(result1);
            Assert.IsNotNull(result1.Data.GetByteStream().GetContentAsByteArray());
            var result2 = taskCompletionSource1.Task.Result;
            Assert.IsNotNull(result2);
            Assert.IsNotNull(result2.Data.GetByteStream().GetContentAsByteArray());
            var result3 = taskCompletionSource1.Task.Result;
            Assert.IsNotNull(result3);
            Assert.IsNotNull(result3.Data.GetByteStream().GetContentAsByteArray());
        }
    }
}