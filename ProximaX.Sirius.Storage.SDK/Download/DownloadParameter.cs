using ProximaX.Sirius.Storage.SDK.PrivacyStrategies;

namespace ProximaX.Sirius.Storage.SDK.Download
{
    public class DownloadParameter
    {
        public string TransactionHash { get; }
        public string AccountPrivateKey { get; }
        public IPrivacyStrategy PrivacyStrategy { get; }
        public bool ValidateDigest { get; }

        internal DownloadParameter(string transactionHash, string accountPrivateKey, IPrivacyStrategy privacyStrategy,
            bool validateDigest)
        {
            TransactionHash = transactionHash;
            AccountPrivateKey = accountPrivateKey;
            PrivacyStrategy = privacyStrategy;
            ValidateDigest = validateDigest;
        }

        public static DownloadParameterBuilder Create(string transactionHash)
        {
            return new DownloadParameterBuilder(transactionHash);
        }
    }
}