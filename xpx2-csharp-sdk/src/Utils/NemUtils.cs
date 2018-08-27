using io.nem2.sdk.Model.Accounts;
using io.nem2.sdk.Model.Blockchain;
using io.nem2.sdk.Model.Transactions;
using System;
using System.Collections.Generic;
using System.Text;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

namespace IO.Proximax.SDK.Utils
{
    public class NemUtils
    {
        private NetworkType.Types NetworkType { get; set; }

        public NemUtils(NetworkType.Types networkType)
        {
            NetworkType = networkType;
        }

        public Address ToAddress(string publicKey)
        {
            CheckParameter(publicKey != null, "publicKey is required");

            return Address.CreateFromPublicKey(publicKey, NetworkType);
        }

        public Account ToAccount(string privateKey)
        {
            CheckParameter(privateKey != null, "privateKey is required");

            return Account.CreateFromPrivateKey(privateKey, NetworkType);
        }

        public SignedTransaction SignTransaction(string signerPrivateKey, Transaction transaction)
        {
            CheckParameter(signerPrivateKey != null, "signerPrivateKey is required");
            CheckParameter(transaction != null, "transaction is required");

            return ToAccount(signerPrivateKey).Sign(transaction);
        }
    }
}
