using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using io.nem2.sdk.Model.Accounts;
using io.nem2.sdk.Model.Blockchain;
using io.nem2.sdk.Model.Transactions;
using IO.Proximax.SDK.Async;
using IO.Proximax.SDK.Connections;
using IO.Proximax.SDK.Exceptions;
using IO.Proximax.SDK.Models;
using IO.Proximax.SDK.Services;
using IO.Proximax.SDK.Services.Clients.Catapult;
using IO.Proximax.SDK.Utils;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

namespace IO.Proximax.SDK.Search
{
    public class Searcher
    {
        private const int BatchTransactionSize = 100;
        
        private NetworkType.Types NetworkType { get; }
        private AccountClient AccountClient { get; }
        private RetrieveProximaxMessagePayloadService RetrieveProximaxMessagePayloadService { get; }

        public Searcher(ConnectionConfig connectionConfig)
        {
            NetworkType = connectionConfig.BlockchainNetworkConnection.NetworkType;
            AccountClient =
                new AccountClient(connectionConfig.BlockchainNetworkConnection);
            RetrieveProximaxMessagePayloadService =
                new RetrieveProximaxMessagePayloadService(connectionConfig.BlockchainNetworkConnection);
        }

        // constructor for unit testing purposes
        internal Searcher(NetworkType.Types networkType,
            AccountClient accountClient,
            RetrieveProximaxMessagePayloadService retrieveProximaxMessagePayloadService)
        {
            NetworkType = networkType;
            AccountClient = accountClient;
            RetrieveProximaxMessagePayloadService = retrieveProximaxMessagePayloadService;
        }

        public SearchResult Search(SearchParameter param)
        {
            CheckParameter(param != null, "param is required");

            return DoSearch(param).Wait();
        }

        public AsyncTask SearchAsync(SearchParameter param, AsyncCallbacks<SearchResult> asyncCallbacks = null)
        {
            CheckParameter(param != null, "param is required");

            var asyncTask = new AsyncTask();

            AsyncUtils.ProcessFirstItem(DoSearch(param), asyncCallbacks, asyncTask);

            return asyncTask;
        }

        private IObservable<SearchResult> DoSearch(SearchParameter param)
        {
            return Observable.Start(() =>
            {
                try
                {
                    var fromTransactionId = param.FromTransactionId;
                    var results = new List<SearchResultItem>();
                    var publicAccount = GetPublicAccount(param.AccountPrivateKey, param.AccountPublicKey,
                        param.AccountAddress);

                    while (results.Count < param.ResultSize)
                    {
                        var transactions = AccountClient.GetTransactions(param.TransactionFilter, BatchTransactionSize,
                            publicAccount,
                            fromTransactionId).Wait();

                        var resultSet = transactions.AsParallel()
                            .Select(txn => ConvertToResultItemIfMatchingCriteria(txn, param))
                            .Where(resultItem => resultItem != null)
                            .Take(param.ResultSize - results.Count)
                            .ToList();

                        results.AddRange(resultSet);

                        // if last fetch is full, there might be more transactions in account
                        // otherwise, search is done
                        if (transactions.Count == BatchTransactionSize)
                        {
                            fromTransactionId = transactions[transactions.Count - 1].TransactionInfo.Id;
                        }
                        else
                        {
                            break;
                        }
                    }

                    var toTransactionId = results.Count == 0 ? null : results[results.Count - 1].TransactionId;
                    return new SearchResult(results, param.FromTransactionId, toTransactionId);
                }
                catch (Exception ex)
                {
                    throw new SearchFailureException("Search failed.", ex);
                }
            });
        }

        private PublicAccount GetPublicAccount(string accountPrivateKey, string accountPublicKey, string accountAddress)
        {
            if (accountPrivateKey != null) {
                return Account.CreateFromPrivateKey(accountPrivateKey, NetworkType).PublicAccount;
            } else if (accountPublicKey != null) {
                return PublicAccount.CreateFromPublicKey(accountPublicKey, NetworkType);
            } else if (accountAddress != null) {
                return PublicAccount.CreateFromPublicKey(AccountClient.GetPublicKey(accountAddress),NetworkType);
            } else {
                throw new ArgumentException("accountPrivateKey, accountPublicKey or accountAddress must be provided");
            }
            
        }
        
        private SearchResultItem ConvertToResultItemIfMatchingCriteria(Transaction transaction, SearchParameter param) 
        {
            if (transaction is TransferTransaction) {
                try {
                    var messagePayload = RetrieveProximaxMessagePayloadService.GetMessagePayload(
                        transaction as TransferTransaction, param.AccountPrivateKey);

                    // verify message payload is upload transaction by having the right json fields
                    if (messagePayload?.Version != null && 
                        messagePayload.PrivacyType != 0 && 
                        messagePayload.Data != null && 
                        messagePayload.Data.Timestamp != 0 && 
                        messagePayload.Data.DataHash != null) {
                        if (MatchesSearchCriteria(messagePayload,
                            param.NameFilter,
                            param.DescriptionFilter,
                            param.MetadataKeyFilter,
                            param.MetadataValueFilter)) {
                            return new SearchResultItem(
                                transaction.TransactionInfo.Hash,
                                transaction.TransactionInfo.Id,
                                messagePayload);
                        }
                    }
                } catch (Exception e) {
                    // skip transaction
                }
            }

            return null;
        }

        private bool MatchesSearchCriteria(ProximaxMessagePayloadModel messagePayload, string nameFilter,
            string descriptionFilter, string metadataKeyFilter, string metadataValueFilter) {
            if (nameFilter != null) {
                if (!(messagePayload.Data.Name != null &&
                      messagePayload.Data.Name.Contains(nameFilter))) {
                    return false;
                }
            }
            if (descriptionFilter != null) {
                if (!(messagePayload.Data.Description != null &&
                      messagePayload.Data.Description.Contains(descriptionFilter))) {
                    return false;
                }
            }
            if (metadataKeyFilter != null) {
                if (metadataValueFilter != null) {
                    if (!(messagePayload.Data.Metadata != null &&
                          metadataValueFilter.Equals(messagePayload.Data.Metadata[metadataKeyFilter]))) {
                        return false;
                    }
                } else {
                    if (!(messagePayload.Data.Metadata != null &&
                          messagePayload.Data.Metadata.ContainsKey(metadataKeyFilter))) {
                        return false;
                    }
                }
            }

            return true;
        }

    }
}