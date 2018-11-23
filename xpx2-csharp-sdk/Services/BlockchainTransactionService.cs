﻿using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using io.nem2.sdk.Model.Accounts;
using io.nem2.sdk.Model.Mosaics;
using io.nem2.sdk.Model.Transactions;
using io.nem2.sdk.Model.Transactions.Messages;
using IO.Proximax.SDK.Connections;
using IO.Proximax.SDK.Exceptions;
using IO.Proximax.SDK.Models;
using IO.Proximax.SDK.Services.Clients.Catapult;
using IO.Proximax.SDK.Utils;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

namespace IO.Proximax.SDK.Services
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

                if (!(transaction.TransactionType.Equals(TransactionTypes.Types.Transfer) &&
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
            bool useBlockchainSecureMessage)
        {
            CheckParameter(signerPrivateKey != null, "signerPrivateKey is required");
            CheckParameter(messagePayload != null, "messagePayload is required");

            var message = BlockchainMessageService.CreateMessage(messagePayload, signerPrivateKey,
                recipientPublicKey, recipientAddress, useBlockchainSecureMessage);
            var recipient = GetRecipient(signerPrivateKey, recipientPublicKey, recipientAddress);
            var transaction = CreateTransaction(recipient, transactionDeadline, message);
            var signedTransaction = NemUtils.SignTransaction(signerPrivateKey, transaction);

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
            IMessage message)
        {
            return TransferTransaction.Create(
                BlockchainNetworkConnection.NetworkType,
                Deadline.CreateHours(transactionDeadline),
                recipientAddress,
                new List<Mosaic> {new Mosaic(new MosaicId("prx:xpx"), 1)},
                message);
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