using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using io.nem2.sdk.Infrastructure.Listeners;
using io.nem2.sdk.Model.Accounts;
using io.nem2.sdk.Model.Blockchain;
using io.nem2.sdk.Model.Mosaics;
using io.nem2.sdk.Model.Transactions;
using IO.Proximax.SDK.Connections;
using IO.Proximax.SDK.Exceptions;
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
				ConnectionConfig.CreateWithLocalIpfsConnection(
					new BlockchainNetworkConnection(BlockchainNetworkType.MijinTest, BlockchainApiHost, BlockchainApiPort, BlockchainApiProtocol),
					new IpfsConnection(IpfsApiHost, IpfsApiPort, BlockchainApiProtocol))
			);
		}

		[TestMethod, Timeout(60000)]
		public void ShouldUploadWithSignerAsRecipientByDefault() {
			var param = UploadParameter
				.CreateForStringUpload(TestString, AccountPrivateKey1)
				.Build();

			var result = UnitUnderTest.Upload(param);

			Assert.IsNotNull(result);
			Assert.IsNotNull(result.TransactionHash);
			var transaction = WaitForTransactionConfirmation(AccountPrivateKey1, result.TransactionHash);
			Assert.IsTrue(transaction is TransferTransaction);
			Assert.AreEqual((transaction as TransferTransaction).Address.Plain, AccountAddress1);
			
			LogAndSaveResult(result, GetType().Name + ".ShouldUploadWithSignerAsRecipientByDefault");
		}

		[TestMethod, Timeout(10000), ExpectedException(typeof(UploadFailureException))]
		public void FailUploadWhenSignerHasNoFunds() {
			var param = UploadParameter
				.CreateForStringUpload(TestString, "345247BEEE41EAF0658530777EC49BA2F74CA849B6A24FDF558B83ABEB8D9DC8")
				.Build();

			UnitUnderTest.Upload(param);
		}

		[TestMethod, Timeout(60000)]
		public void ShouldUploadWithRecipientPublicKeyProvided() {
			var param = UploadParameter
				.CreateForStringUpload(TestString, AccountPrivateKey1)
				.WithRecipientPublicKey(AccountPublicKey2)
				.Build();


			var result = UnitUnderTest.Upload(param);

			Assert.IsNotNull(result);
			Assert.IsNotNull(result.TransactionHash);
			var transaction = WaitForTransactionConfirmation(AccountPrivateKey1, result.TransactionHash);
			Assert.IsTrue(transaction is TransferTransaction);
			Assert.AreEqual((transaction as TransferTransaction).Address.Plain, AccountAddress2);
			
			LogAndSaveResult(result, GetType().Name + ".ShouldUploadWithRecipientPublicKeyProvided");
		}

		[TestMethod, Timeout(60000)]
		public void ShouldUploadWithRecipientAddressProvided() {
			var param = UploadParameter
				.CreateForStringUpload(TestString, AccountPrivateKey1)
				.WithRecipientAddress(AccountAddress2)
				.Build();

			var result = UnitUnderTest.Upload(param);
 
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.TransactionHash);
			var transaction = WaitForTransactionConfirmation(AccountPrivateKey1, result.TransactionHash);
			Assert.IsTrue(transaction is TransferTransaction);
			Assert.AreEqual((transaction as TransferTransaction).Address.Plain, AccountAddress2);
			
			LogAndSaveResult(result, GetType().Name + ".ShouldUploadWithRecipientAddressProvided");
		}

		[TestMethod, Timeout(30000)]
		public void ShouldUploadWithTransactionDeadlinesProvided() {
			var param = UploadParameter
				.CreateForStringUpload(TestString, AccountPrivateKey1)
				.WithTransactionDeadline(2)
				.Build();

			var result = UnitUnderTest.Upload(param);

			Assert.IsNotNull(result);
			Assert.IsNotNull(result.TransactionHash);
			var transaction = WaitForTransactionConfirmation(AccountPrivateKey1, result.TransactionHash);
			Assert.IsTrue(transaction is TransferTransaction);
			Assert.IsTrue((transaction as TransferTransaction).Deadline.GetInstant() <= Deadline.CreateHours(2).GetInstant());
			
			LogAndSaveResult(result, GetType().Name + ".ShouldUploadWithTransactionDeadlinesProvided");
		}
		
		[TestMethod, Timeout(30000)]
		public void ShouldUploadWithTransactionMosaicsProvided() {
			var param = UploadParameter
				.CreateForStringUpload(TestString, AccountPrivateKey1)
				.WithTransactionMosaics(new List<Mosaic> {new Mosaic(new MosaicId("prx:xpx"), 2)})
				.Build();

			var result = UnitUnderTest.Upload(param);

			var transaction = WaitForTransactionConfirmation(AccountPrivateKey1, result.TransactionHash);
			Assert.IsTrue(transaction is TransferTransaction);
			Assert.AreEqual((transaction as TransferTransaction).Mosaics.Count, 1);
			Assert.AreEqual((transaction as TransferTransaction).Mosaics[0].MosaicId, new MosaicId("prx:xpx"));
			Assert.AreEqual((transaction as TransferTransaction).Mosaics[0].Amount, (ulong) 2);
			
			LogAndSaveResult(result, GetType().Name + ".ShouldUploadWithTransactionMosaicsProvided");
		}
		
		[TestMethod, Timeout(30000)]
		public void ShouldUploadWithEmptyTransactionMosaicsProvided() {
			var param = UploadParameter
				.CreateForStringUpload(TestString, AccountPrivateKey1)
				.WithTransactionMosaics(new List<Mosaic>())
				.Build();

			var result = UnitUnderTest.Upload(param);

			var transaction = WaitForTransactionConfirmation(AccountPrivateKey1, result.TransactionHash);
			Assert.IsTrue(transaction is TransferTransaction);
			Assert.AreEqual((transaction as TransferTransaction).Mosaics.Count, 0);
			
			LogAndSaveResult(result, GetType().Name + ".ShouldUploadWithEmptyTransactionMosaicsProvided");
		}
		
		private Transaction WaitForTransactionConfirmation(string senderPrivateKey, string transactionHash) {
			var listener = new Listener(new Uri(BlockchainRestApiUrl).Host);
			try
			{
				listener.Open().Wait();
				var transaction = listener.ConfirmedTransactionsGiven(
						Account.CreateFromPrivateKey(senderPrivateKey, NetworkType.Types.MIJIN_TEST).Address)
					.Distinct(unconfirmedTxn => unconfirmedTxn.TransactionInfo.Hash.Equals(transactionHash))
					.FirstAsync()
					.Wait();
				return transaction;
			}
			catch (Exception e)
			{
				throw new Exception("Failed to listen on confirmed transaction", e);
			}
			finally
			{
				listener?.Close();
			}
		}
	}
}