using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using io.nem2.sdk.Infrastructure.Buffers.Model;
using io.nem2.sdk.Infrastructure.HttpRepositories;
using io.nem2.sdk.Model.Accounts;
using io.nem2.sdk.Model.Transactions;
using Proximax.Storage.SDK.Connections;
using Proximax.Storage.SDK.Exceptions;
using Proximax.Storage.SDK.Models;
using static Proximax.Storage.SDK.Utils.ParameterValidationUtils;

namespace Proximax.Storage.SDK.Services.Clients.Catapult
{
    public class AccountClient
    {
        /**
         * The public key constant when it is not yet used to send transaction on catapult.
         */
        public const string PublicKeyNotFound = "0000000000000000000000000000000000000000000000000000000000000000";

        private AccountHttp AccountHttp { get; set; }

        public AccountClient(BlockchainNetworkConnection blockchainNetworkConnection)
        {
            AccountHttp = new AccountHttp(blockchainNetworkConnection.RestApiUrl);
        }

        // constructor for unit testing purposes
        internal AccountClient(AccountHttp accountHttp)
        {
            AccountHttp = accountHttp;
        }

        public string GetPublicKey(string address)
        {
            CheckParameter(address != null, "address is required");

            try
            {
                var accountInfo = AccountHttp.GetAccountInfo(Address.CreateFromRawAddress(address)).Wait();
                if (accountInfo.PublicKey.Equals(PublicKeyNotFound))
                    throw new PublicKeyNotFoundException($"Address {address} has no public key yet on blockchain");
                return accountInfo.PublicKey;
            }
            catch (PublicKeyNotFoundException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw new AccountNotFoundException(
                    $"Failed to retrieve account for {address}. Probably address is not yet revealed on blockchain.",
                    e);
            }
        }

        public IObservable<IList<Transaction>> GetTransactions(TransactionFilter transactionFilter, int resultSize,
            PublicAccount publicAccount, string fromTransactionId = null)
        {
            var queryParams = new QueryParams(resultSize, fromTransactionId);

            switch (transactionFilter)
            {
                case TransactionFilter.All:
                    return AccountHttp.Transactions(publicAccount, queryParams);
                case TransactionFilter.Outgoing:
                    return AccountHttp.OutgoingTransactions(publicAccount, queryParams);
                case TransactionFilter.Incoming:
                    return AccountHttp.IncomingTransactions(publicAccount, queryParams);
                default:
                    throw new Exception($"Unknown transactionFilter {transactionFilter}");
            }
        }
    }
}