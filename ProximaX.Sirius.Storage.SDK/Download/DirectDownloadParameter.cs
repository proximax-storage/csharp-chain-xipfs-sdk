using ProximaX.Sirius.Storage.SDK.PrivacyStrategies;

namespace ProximaX.Sirius.Storage.SDK.Download
{
    public class DirectDownloadParameter
    {
        public string TransactionHash { get; }
        public string AccountPrivateKey { get; }
        public string DataHash { get; }
        public bool ValidateDigest { get; }
        public IPrivacyStrategy PrivacyStrategy { get; }
        public string Digest { get; }

        internal DirectDownloadParameter(string transactionHash, string accountPrivateKey, string dataHash,
            bool validateDigest, IPrivacyStrategy privacyStrategy, string digest)
        {
            TransactionHash = transactionHash;
            AccountPrivateKey = accountPrivateKey;
            DataHash = dataHash;
            ValidateDigest = validateDigest;
            PrivacyStrategy = privacyStrategy;
            Digest = digest;
        }

        public static DirectDownloadParameterBuilder CreateFromTransactionHash(string transactionHash,
            string accountPrivateKey = null, bool? validateDigest = null)
        {
            return DirectDownloadParameterBuilder.CreateFromTransactionHash(transactionHash, accountPrivateKey,
                validateDigest);
        }

        public static DirectDownloadParameterBuilder CreateFromDataHash(string dataHash, string digest = null)
        {
            return DirectDownloadParameterBuilder.CreateFromDataHash(dataHash, digest);
        }
    }
}