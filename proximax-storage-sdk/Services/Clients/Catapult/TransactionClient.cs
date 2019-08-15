using System;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using Proximax.Storage.SDK.Connections;
using Proximax.Storage.SDK.Exceptions;
using ProximaX.Sirius.Chain.Sdk.Infrastructure;
using ProximaX.Sirius.Chain.Sdk.Infrastructure.Listener;
using ProximaX.Sirius.Chain.Sdk.Model.Accounts;
using ProximaX.Sirius.Chain.Sdk.Model.Blockchain;
using ProximaX.Sirius.Chain.Sdk.Model.Transactions;
using static Proximax.Storage.SDK.Utils.ParameterValidationUtils;

namespace Proximax.Storage.SDK.Services.Clients.Catapult
{
    public class TransactionClient
    {
        private const string StatusForSuccessfulUnconfirmedTransaction = "SUCCESS";

        private TransactionHttp TransactionHttp { get; set; }

        private BlockHttp BlockHttp { get; set; }

        private string BlockchainRestApiHost { get; set; }
        private int BlockchainRestApiPort { get; set; }
        private Listener Listener { get; set; }

        public TransactionClient(BlockchainNetworkConnection blockchainNetworkConnection)
        {
            TransactionHttp = new TransactionHttp(blockchainNetworkConnection.RestApiUrl);
            BlockHttp = new BlockHttp(blockchainNetworkConnection.RestApiUrl);
            var uri = new Uri(blockchainNetworkConnection.RestApiUrl);
            BlockchainRestApiHost = uri.Host;
            BlockchainRestApiPort = uri.Port;
        }

        internal TransactionClient(TransactionHttp transactionHttp, Listener listener)
        {
            TransactionHttp = transactionHttp;
            Listener = listener;
        }

        public string Announce(SignedTransaction signedTransaction, Address address)
        {
            CheckParameter(signedTransaction != null, "signedTransaction is required");
            CheckParameter(address != null, "address is required");

            var listener = GetListener();
            lock (listener)
            {
                listener.Open().Wait();
                try
                {
                    var failedTransactionStatusOb =
                        GetAddedFailedTransactionStatus(address, signedTransaction.Hash, listener);
                    var unconfirmedTransactionStatusOb =
                        GetAddedUnconfirmedTransactionStatus(address, signedTransaction.Hash, listener);

                    var firstTxnStatusTask = failedTransactionStatusOb.Merge(unconfirmedTransactionStatusOb).Select(
                        status =>
                        {
                            if (status.Equals(StatusForSuccessfulUnconfirmedTransaction))
                                return status;
                            else
                                throw new AnnounceBlockchainTransactionFailureException(
                                    $"Failed to announce transaction with status {status}");
                        }).FirstAsync().ToTask();

                    TransactionHttp.Announce(signedTransaction).Wait();
                    firstTxnStatusTask.Wait();
                    return firstTxnStatusTask.Result;
                }
                catch (AnnounceBlockchainTransactionFailureException e)
                {
                    throw e;
                }
                catch (Exception e)
                {
                    throw new AnnounceBlockchainTransactionFailureException("Failed to announce transaction", e);
                }
                finally
                {
                    this.CloseListener(listener);
                }
            }
        }

        public IObservable<Transaction> GetTransaction(string transactionHash)
        {
            CheckParameter(transactionHash != null, "transactionHash is required");

            return TransactionHttp.GetTransaction(transactionHash);
        }

        public IObservable<BlockInfo> GetNemesisBlockInfo()
        {
            return BlockHttp.GetBlockByHeight(1);
        }


        private Listener GetListener()
        {
            return Listener ?? new Listener(BlockchainRestApiHost, BlockchainRestApiPort);
        }

        private IObservable<string> GetAddedUnconfirmedTransactionStatus(Address address, string transactionHash,
            Listener listener)
        {
            return listener.UnconfirmedTransactionsAdded(address)
                .Where(unconfirmedTxn =>
                    unconfirmedTxn.TransactionInfo != null &&
                    unconfirmedTxn.TransactionInfo.Hash.Equals(transactionHash))
                .Select(unconfirmedTxn => StatusForSuccessfulUnconfirmedTransaction);
        }

        private IObservable<string> GetAddedFailedTransactionStatus(Address address, string transactionHash,
            Listener listener)
        {
            return listener.TransactionStatus(address)
                .Where(transactionStatusError =>
                    transactionStatusError.Hash.Equals(transactionHash))
                .Select(transactionStatusError => transactionStatusError.Status);
        }

        private void CloseListener(Listener listener)
        {
            try
            {
                // Unable to read data from the transport connection:
                // An existing connection was forcibly closed by the remote host.
                listener.Close();
            }
            catch (Exception)
            {
                Console.WriteLine("failed to close listener");
                // ignore exception
            }
        }
    }
}