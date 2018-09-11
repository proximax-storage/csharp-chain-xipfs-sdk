using System;
using System.Reactive.Linq;
using io.nem2.sdk.Infrastructure.Buffers.Model;
using io.nem2.sdk.Infrastructure.HttpRepositories;
using io.nem2.sdk.Infrastructure.Listeners;
using io.nem2.sdk.Model.Accounts;
using io.nem2.sdk.Model.Transactions;
using IO.Proximax.SDK.Connections;
using IO.Proximax.SDK.Exceptions;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

namespace IO.Proximax.SDK.Services.Clients
{
    public class TransactionClient
    {
        private const string STATUS_FOR_SUCCESSFUL_UNCONFIRMED_TRANSACTION = "SUCCESS";
        
        private TransactionHttp TransactionHttp { get; set; }
        private string BlockchainNetworkEndpointUrl { get; set; }
        private Listener Listener { get; set; }

        public TransactionClient(BlockchainNetworkConnection blockchainNetworkConnection)
        {
            TransactionHttp = new TransactionHttp(blockchainNetworkConnection.EndpointUrl);
            BlockchainNetworkEndpointUrl = blockchainNetworkConnection.EndpointUrl;
            Listener = null;
        }

        internal TransactionClient(TransactionHttp transactionHttp, Listener listener)
        {
            TransactionHttp = transactionHttp;
            Listener = listener;
            BlockchainNetworkEndpointUrl = null;
        }

        public IObservable<TransactionAnnounceResponse> Announce(SignedTransaction signedTransaction)
        {
            CheckParameter(signedTransaction != null, "signedTransaction is required");

            return TransactionHttp.Announce(signedTransaction);
        }

        public IObservable<Transaction> GetTransaction(string transactionHash)
        {
            CheckParameter(transactionHash != null, "transactionHash is required");

            return TransactionHttp.GetTransaction(transactionHash);
        }

        public string WaitForAnnouncedTransactionToBeUnconfirmed(Address address, string transactionHash)
        {
            CheckParameter(address != null, "address is required");
            CheckParameter(transactionHash != null, "transactionHash is required");

            Listener listener = GetListener();
            lock (listener)
            {
                listener.Open().Wait();

                var failedTransactionStatusOb = GetAddedFailedTransactionStatus(address, transactionHash, listener);
                var unconfirmedTransactionStatusOb = GetAddedUnconfirmedTransactionStatus(address, transactionHash, listener);

                return failedTransactionStatusOb.Merge(unconfirmedTransactionStatusOb).Select(status => {
                    if (status.Equals(STATUS_FOR_SUCCESSFUL_UNCONFIRMED_TRANSACTION))
                        return status;
                    else
                        throw new AnnounceBlockchainTransactionFailureException(
                                $"Failed to announce transaction with status {status}");
                }).FirstAsync().Wait();
            }

        }

        private Listener GetListener()
        {
            return Listener ?? new Listener(BlockchainNetworkEndpointUrl);
        }

        private IObservable<string> GetAddedUnconfirmedTransactionStatus(Address address, string transactionHash, Listener listener)
        {
            return listener.UnconfirmedTransactionsAdded(address)
                    .Where(unconfirmedTxn =>
                            unconfirmedTxn.TransactionInfo != null &&
                                    unconfirmedTxn.TransactionInfo.Hash.Equals(transactionHash))
                    .Select(unconfirmedTxn => STATUS_FOR_SUCCESSFUL_UNCONFIRMED_TRANSACTION);
        }

        private IObservable<string> GetAddedFailedTransactionStatus(Address address, string transactionHash, Listener listener)
        {
            return listener.TransactionStatus(address)
                    .Where(transactionStatusError =>
                            transactionStatusError.Hash.Equals(transactionHash))
                    .Select(transactionStatusError => transactionStatusError.Status);
        }

    }
}
