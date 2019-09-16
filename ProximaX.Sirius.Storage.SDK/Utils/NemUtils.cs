
using ProximaX.Sirius.Chain.Sdk.Model.Accounts;
using ProximaX.Sirius.Chain.Sdk.Model.Blockchain;
using ProximaX.Sirius.Chain.Sdk.Model.Transactions;
using static ProximaX.Sirius.Storage.SDK.Utils.ParameterValidationUtils;

namespace ProximaX.Sirius.Storage.SDK.Utils
{
    public class NemUtils
    {
        private NetworkType NetworkType { get; set; }

        public NemUtils(NetworkType networkType)
        {
            NetworkType = networkType;
        }

        public Address GetAddress(string address)
        {
            CheckParameter(address != null, "address is required");

            return Address.CreateFromRawAddress(address);
        }

        public Address GetAddressFromPublicKey(string publicKey)
        {
            CheckParameter(publicKey != null, "publicKey is required");

            return Address.CreateFromPublicKey(publicKey, NetworkType);
        }

        public Address GetAddressFromPrivateKey(string privateKey)
        {
            CheckParameter(privateKey != null, "privateKey is required");

            return GetAccount(privateKey).Address;
        }

        public Account GetAccount(string privateKey)
        {
            CheckParameter(privateKey != null, "privateKey is required");

            return Account.CreateFromPrivateKey(privateKey, NetworkType);
        }

        public SignedTransaction SignTransaction(string signerPrivateKey, Transaction transaction,string generationHash)
        {
            CheckParameter(signerPrivateKey != null, "signerPrivateKey is required");
            CheckParameter(transaction != null, "transaction is required");

            return GetAccount(signerPrivateKey).Sign(transaction, generationHash);
        }
    }
}