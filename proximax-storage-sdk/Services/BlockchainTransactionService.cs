using System;
using System.Collections.Generic;
using System.Reactive.Linq;

using Proximax.Storage.SDK.Connections;
using Proximax.Storage.SDK.Exceptions;
using Proximax.Storage.SDK.Models;
using Proximax.Storage.SDK.Services.Clients.Catapult;
using Proximax.Storage.SDK.Utils;
using ProximaX.Sirius.Chain.Sdk.Model.Accounts;
using ProximaX.Sirius.Chain.Sdk.Model.Mosaics;
using ProximaX.Sirius.Chain.Sdk.Model.Transactions;
using ProximaX.Sirius.Chain.Sdk.Model.Transactions.Messages;
using static Proximax.Storage.SDK.Utils.ParameterValidationUtils;

namespace Proximax.Storage.SDK.Services
{
    public class BlockchainTransactionService
    {
        private BlockchainNetworkConnection BlockchainNetworkConnection { get; }
        private BlockchainMessageService BlockchainMessageService { get; }

        private TransactionClient TransactionClient { get; }
        private NemUtils NemUtils { get; }

        public BlockchainTransactionService(BlockchainNetworkConnection blockchainNetworkConnection)
        {
            BlockchainNetworkConnection = blockchainNetworkConnection;
            TransactionClient = new TransactionClient(blockchainNetworkConnection);
            NemUtils = new NemUtils(blockchainNetworkConnection.NetworkType);
            BlockchainMessageService = new BlockchainMessageService(blockchainNetworkConnection);
        }

        internal BlockchainTransactionService(BlockchainNetworkConnection blockchainNetworkConnection,
            BlockchainMessageService blockchainMessageService,
            TransactionClient transactionClient, NemUtils nemUtils)
        {
            BlockchainNetworkConnection = blockchainNetworkConnection;
            BlockchainMessageService = blockchainMessageService;
            TransactionClient = transactionClient;
            NemUtils = nemUtils;
        }

        public IObservable<TransferTransaction> GetTransferTransaction(string transactionHash)
        {
            CheckParameter(transactionHash != null, "transactionHash is required");

            try
            {
                var transaction = GetTransaction(transactionHash);

                if (!(transaction.TransactionType.Equals(TransactionType.TRANSFER) &&
                      transaction is TransferTransaction))
                    throw new NotSupportedException("Expecting a transfer transaction");

                return Observable.Return(transaction as TransferTransaction);
            }
            catch (Exception ex)
            {
                return Observable.Throw<TransferTransaction>(ex);
            }
        }

        public IObservable<string> CreateAndAnnounceTransaction(ProximaxMessagePayloadModel messagePayload,
            string signerPrivateKey,
            string recipientPublicKey, string recipientAddress, int transactionDeadline,
            List<Mosaic> transactionMosaics, bool useBlockchainSecureMessage)
        {
            CheckParameter(signerPrivateKey != null, "signerPrivateKey is required");
            CheckParameter(messagePayload != null, "messagePayload is required");

            var message = BlockchainMessageService.CreateMessage(messagePayload, signerPrivateKey,
                recipientPublicKey, recipientAddress, useBlockchainSecureMessage);
            var recipient = GetRecipient(signerPrivateKey, recipientPublicKey, recipientAddress);
            var transaction = CreateTransaction(recipient, transactionDeadline, transactionMosaics, message);
            var generationHash = TransactionClient.GetNemesisBlockInfo().Wait().GenerationHash;
            var signedTransaction = NemUtils.SignTransaction(signerPrivateKey, transaction, generationHash);
            
            TransactionClient.Announce(signedTransaction, NemUtils.GetAddressFromPrivateKey(signerPrivateKey));

            return Observable.Return(signedTransaction.Hash);
        }

        private Address GetRecipient(string signerPrivateKey, string recipientPublicKey, string recipientAddress)
        {
            if (recipientPublicKey != null)
            {
                return NemUtils.GetAddressFromPublicKey(recipientPublicKey);
            }
            else if (recipientAddress != null)
            {
                return NemUtils.GetAddress(recipientAddress);
            }
            else
            {
                return NemUtils.GetAddressFromPrivateKey(signerPrivateKey);
            }
        }

        private TransferTransaction CreateTransaction(Address recipientAddress, int transactionDeadline,
            List<Mosaic> transactionMosaics, IMessage message)
        {
            return TransferTransaction.Create(
                Deadline.Create(transactionDeadline),
                recipientAddress,
                transactionMosaics ?? new List<Mosaic> { NetworkCurrencyMosaic.CreateRelative(0) },
                message,
                BlockchainNetworkConnection.NetworkType
                );
        }

        private Transaction GetTransaction(string transactionHash)
        {
            try
            {
                return TransactionClient.GetTransaction(transactionHash).Wait();
            }
            catch (Exception ex)
            {
                throw new GetTransactionFailureException($"Unable to get transaction for {transactionHash}", ex);
            }
        }
    }
}