using System;
using System.Collections.Generic;
using System.Reactive.Linq;

using ProximaX.Sirius.Storage.SDK.Connections;
using ProximaX.Sirius.Storage.SDK.Exceptions;
using ProximaX.Sirius.Storage.SDK.Models;
using ProximaX.Sirius.Storage.SDK.Services.Clients.Catapult;
using ProximaX.Sirius.Storage.SDK.Utils;
using ProximaX.Sirius.Chain.Sdk.Model.Accounts;
using ProximaX.Sirius.Chain.Sdk.Model.Mosaics;
using ProximaX.Sirius.Chain.Sdk.Model.Transactions;
using ProximaX.Sirius.Chain.Sdk.Model.Transactions.Messages;
using static ProximaX.Sirius.Storage.SDK.Utils.ParameterValidationUtils;
using ProximaX.Sirius.Chain.Sdk.Model.Transactions.Builders;
using ProximaX.Sirius.Chain.Sdk.Model.Fees;

namespace ProximaX.Sirius.Storage.SDK.Services
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

                if (!(transaction.TransactionType.Equals(EntityType.TRANSFER) &&
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
            var mosaics = (transactionMosaics == null || transactionMosaics.Count <= 0) ? new List<Mosaic> { NetworkCurrencyMosaic.CreateRelative(0) } : transactionMosaics;

            var recipient = Recipient.From(recipientAddress);

            var builder = new TransferTransactionBuilder();
           
            var transferTransaction = builder
                .SetNetworkType(BlockchainNetworkConnection.NetworkType)
                .SetDeadline(Deadline.Create(transactionDeadline))
                .SetMosaics(mosaics)
                .SetRecipient(recipient)
                .SetMessage(message)
                .SetFeeCalculationStrategy(BlockchainNetworkConnection.FeeCalculationStrategy)
                .Build();

            return transferTransaction;
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