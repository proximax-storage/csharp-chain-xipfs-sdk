using io.nem2.sdk.Model.Transactions;
using IO.Proximax.SDK.Connections;
using IO.Proximax.SDK.Exceptions;
using IO.Proximax.SDK.Services.Clients;
using IO.Proximax.SDK.Services.Factories;
using IO.Proximax.SDK.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

namespace IO.Proximax.SDK.Services
{
    public class BlockchainTransactionService
    {
        private BlockchainNetworkConnection BlockchainNetworkConnection { get; set; }
        private BlockchainMessageFactory BlockchainMessageFactory { get; set; }
        private TransactionClient TransactionClient { get; set; }
        private NemUtils NemUtils { get; set; }

        public BlockchainTransactionService(BlockchainNetworkConnection blockchainNetworkConnection)
        {
            BlockchainNetworkConnection = blockchainNetworkConnection;
            TransactionClient = new TransactionClient(blockchainNetworkConnection);
            NemUtils = new NemUtils(blockchainNetworkConnection.NetworkType);
            BlockchainMessageFactory = new BlockchainMessageFactory();
        }

        internal BlockchainTransactionService(BlockchainNetworkConnection blockchainNetworkConnection, BlockchainMessageFactory blockchainMessageFactory, TransactionClient transactionClient, NemUtils nemUtils)
        {
            BlockchainNetworkConnection = blockchainNetworkConnection;
            BlockchainMessageFactory = blockchainMessageFactory;
            TransactionClient = transactionClient;
            NemUtils = nemUtils;
        }

        public IObservable<TransferTransaction> GetTransferTransaction(string transactionHash)
        {
            CheckParameter(transactionHash != null, "transactionHash is required");

            try
            {
                Transaction transaction = GetTransaction(transactionHash);

                if (!(transaction.TransactionType.Equals(TransactionTypes.Types.Transfer) &&
                        transaction is TransferTransaction))
                    throw new TransactionNotAllowedException("Expecting a transfer transaction");

                return Observable.Return(transaction as TransferTransaction);
            } catch (Exception ex)
            {
                return Observable.Throw<TransferTransaction>(ex);
            }

        }

        public Transaction GetTransaction(string transactionHash)
        {
            try
            {
                return TransactionClient.GetTransaction(transactionHash).Wait();
            } catch (Exception ex)
            {
                throw new GetTransactionFailureException($"Unable to get transaction for {transactionHash}", ex);
            }
        }
    }
}
