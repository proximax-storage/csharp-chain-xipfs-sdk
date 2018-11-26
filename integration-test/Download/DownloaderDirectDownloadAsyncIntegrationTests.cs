using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using IO.Proximax.SDK.Async;
using IO.Proximax.SDK.Connections;
using IO.Proximax.SDK.Download;
using IO.Proximax.SDK.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static IntegrationTests.IntegrationTestConfig;

namespace IntegrationTests.Upload
{
	[TestClass]
	public class DownloaderDirectDownloadAsyncIntegrationTests
	{
		private Downloader UnitUnderTest { get; set; }

		[TestInitialize]
		public void TestInitialize()
		{
			UnitUnderTest = new Downloader(
				ConnectionConfig.CreateWithLocalIpfsConnection(
					new BlockchainNetworkConnection(BlockchainNetworkType.MijinTest, BlockchainApiHost, BlockchainApiPort, BlockchainApiProtocol),
					new IpfsConnection(IpfsApiHost, IpfsApiPort, BlockchainApiProtocol))
			);
		}

		[TestMethod, Timeout(30000)]
		public void ShouldDirectDownloadAsynchronouslyWithoutCallback() {
			var dataHash = TestDataRepository
				.GetData("UploaderIntegrationTests.ShouldUploadByteArray", "dataHash");
			var param = DirectDownloadParameter.CreateFromDataHash(dataHash).Build();
	
			var asyncTask = UnitUnderTest.DirectDownloadAsync(param);

			while (!asyncTask.IsDone()) {
				Thread.Sleep(50);
			}
	
			Assert.IsTrue(asyncTask.IsDone());
		}

		[TestMethod, Timeout(30000)]
		public void ShouldDirectDownloadAsynchronouslyWithSuccessCallback() {
			var dataHash = TestDataRepository
				.GetData("UploaderIntegrationTests.ShouldUploadByteArray", "dataHash");
			var param = DirectDownloadParameter.CreateFromDataHash(dataHash).Build();
	
			var taskCompletionSource = new TaskCompletionSource<Stream>();
			var asyncCallbacks = AsyncCallbacks<Stream>.Create<Stream>(
				streamResult => taskCompletionSource.SetResult(streamResult), null);

			UnitUnderTest.DirectDownloadAsync(param, asyncCallbacks);
			taskCompletionSource.Task.Wait(5000);
			
			var result = taskCompletionSource.Task.Result;
			Assert.IsNotNull(result);
			Assert.IsNotNull(new StreamReader(result).ReadToEnd());
		}

		[TestMethod, Timeout(10000)]
		public void ShouldDownloadAsynchronouslyWithFailureCallback() {
			var param = DirectDownloadParameter.CreateFromDataHash("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA").Build();
			var taskCompletionSource = new TaskCompletionSource<Exception>();
			var asyncCallbacks = AsyncCallbacks<Stream>.Create<Stream>(
				null, ex => taskCompletionSource.SetResult(ex));

			UnitUnderTest.DirectDownloadAsync(param, asyncCallbacks);
			taskCompletionSource.Task.Wait(5000);

			var exception = taskCompletionSource.Task.Result;
			
			Assert.IsInstanceOfType(exception, exception.GetType());
		}

		[TestMethod, Timeout(30000)]
		public void ShouldDirectDownloadMultipleRequestsAsynchronously() {
			var dataHash = TestDataRepository
				.GetData("UploaderIntegrationTests.ShouldUploadByteArray", "dataHash");
			var param = DirectDownloadParameter.CreateFromDataHash(dataHash).Build();;
	
			var taskCompletionSource1 = new TaskCompletionSource<Stream>();
			var taskCompletionSource2 = new TaskCompletionSource<Stream>();
			var taskCompletionSource3 = new TaskCompletionSource<Stream>();
			var asyncCallbacks1 = AsyncCallbacks<Stream>.Create<Stream>(
				streamResult => taskCompletionSource1.SetResult(streamResult), null);
			var asyncCallbacks2 = AsyncCallbacks<Stream>.Create<Stream>(
				streamResult => taskCompletionSource2.SetResult(streamResult), null);
			var asyncCallbacks3 = AsyncCallbacks<Stream>.Create<Stream>(
				streamResult => taskCompletionSource3.SetResult(streamResult), null);

			UnitUnderTest.DirectDownloadAsync(param, asyncCallbacks1);
			UnitUnderTest.DirectDownloadAsync(param, asyncCallbacks2);
			UnitUnderTest.DirectDownloadAsync(param, asyncCallbacks3);
			taskCompletionSource1.Task.Wait(5000);
			taskCompletionSource2.Task.Wait(5000);
			taskCompletionSource3.Task.Wait(5000);
			
			var result1 = taskCompletionSource1.Task.Result;
			Assert.IsNotNull(result1);
			Assert.IsNotNull(new StreamReader(result1).ReadToEnd());
			var result2 = taskCompletionSource1.Task.Result;
			Assert.IsNotNull(result2);
			Assert.IsNotNull(new StreamReader(result2).ReadToEnd());
			var result3 = taskCompletionSource1.Task.Result;
			Assert.IsNotNull(result3);
			Assert.IsNotNull(new StreamReader(result3).ReadToEnd());
		}

	}
}