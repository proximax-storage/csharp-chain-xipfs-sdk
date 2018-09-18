using System;
using System.Reactive.Linq;
using io.nem2.sdk.Infrastructure.Listeners;
using io.nem2.sdk.Model.Accounts;
using io.nem2.sdk.Model.Blockchain;
using io.nem2.sdk.Model.Transactions;
using IO.Proximax.SDK.Connections;
using IO.Proximax.SDK.Models;
using IO.Proximax.SDK.Upload;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static IntegrationTests.IntegrationTestConfig;
using static IntegrationTests.TestDataRepository;
using static IntegrationTests.TestSupport.Constants;

namespace IntegrationTests.Upload
{
	[TestClass]
	public class UploaderTransactionConfigIntegrationTests
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

		[TestMethod, Timeout(30000)]
		public void ShouldUploadWithSignerAsRecipientByDefault() {
			var param = UploadParameter
				.CreateForStringUpload(TestString, PrivateKey1)
				.Build();

			var result = UnitUnderTest.Upload(param);

			Assert.IsNotNull(result);
			Assert.IsNotNull(result.TransactionHash);
			var transaction = WaitForTransactionConfirmation(PrivateKey1, result.TransactionHash);
			Assert.IsTrue(transaction is TransferTransaction);
			Assert.AreEqual((transaction as TransferTransaction).Address.Plain, Address1);
			
			LogAndSaveResult(result, GetType().Name + ".ShouldUploadWithSignerAsRecipientByDefault");
		}


		[TestMethod, Timeout(30000)]
		public void ShouldUploadWithRecipientPublicKeyProvided() {
			var param = UploadParameter
				.CreateForStringUpload(TestString, PrivateKey1)
				.WithRecipientPublicKey(PublicKey2)
				.Build();


			var result = UnitUnderTest.Upload(param);

			Assert.IsNotNull(result);
			Assert.IsNotNull(result.TransactionHash);
			var transaction = WaitForTransactionConfirmation(PrivateKey1, result.TransactionHash);
			Assert.IsTrue(transaction is TransferTransaction);
			Assert.AreEqual((transaction as TransferTransaction).Address.Plain, Address2);
			
			LogAndSaveResult(result, GetType().Name + ".ShouldUploadWithRecipientPublicKeyProvided");
		}

		[TestMethod, Timeout(30000)]
		public void ShouldUploadWithRecipientAddressProvided() {
			var param = UploadParameter
				.CreateForStringUpload(TestString, PrivateKey1)
				.WithRecipientAddress(Address2)
				.Build();

			var result = UnitUnderTest.Upload(param);

			Assert.IsNotNull(result);
			Assert.IsNotNull(result.TransactionHash);	
			var transaction = WaitForTransactionConfirmation(PrivateKey1, result.TransactionHash);
			Assert.IsTrue(transaction is TransferTransaction);
			Assert.AreEqual((transaction as TransferTransaction).Address.Plain, Address2);
			
			LogAndSaveResult(result, GetType().Name + ".ShouldUploadWithRecipientAddressProvided");
		}

		[TestMethod, Timeout(10000)]
		public void ShouldUploadWithTransactionDeadlinesProvided() {
			var param = UploadParameter
				.CreateForStringUpload(TestString, PrivateKey1)
				.WithTransactionDeadline(2)
				.Build();

			var result = UnitUnderTest.Upload(param);

			Assert.IsNotNull(result);
			Assert.IsNotNull(result.TransactionHash);
			
			LogAndSaveResult(result, GetType().Name + ".ShouldUploadWithTransactionDeadlinesProvided");
		}

		[TestMethod, Timeout(10000)]
		public void ShouldUploadWithUseBlockchainSecureMessageProvided() {
			var param = UploadParameter
				.CreateForStringUpload(TestString, PrivateKey1)
				.WithUseBlockchainSecureMessage(true)
				.Build();

			var result = UnitUnderTest.Upload(param);

			Assert.IsNotNull(result);
			Assert.IsNotNull(result.TransactionHash);
			
			LogAndSaveResult(result, GetType().Name + ".ShouldUploadWithUseBlockchainSecureMessageProvided");
		}
		
		private Transaction WaitForTransactionConfirmation(string senderPrivateKey, string transactionHash) {
			try {
				var listener = new Listener(new Uri(BlockchainRestApiUrl).Host);
				listener.Open().Wait();
				var transaction = listener.ConfirmedTransactionsGiven(
						Account.CreateFromPrivateKey(senderPrivateKey, NetworkType.Types.MIJIN_TEST).Address)
					.Distinct(unconfirmedTxn => unconfirmedTxn.TransactionInfo.Hash.Equals(transactionHash))
					.FirstAsync()
					.Wait();
				//listener.Close();
				return transaction;
			} catch (Exception e) {
				throw new Exception("Failed to listen on confirmed transaction", e);
			}
		}
	}
}