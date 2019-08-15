using ProximaX.Sirius.Storage.SDK.PrivacyStrategies;
using ProximaX.Sirius.Chain.Sdk.Model.Accounts;
using static ProximaX.Sirius.Storage.SDK.Utils.ParameterValidationUtils;

namespace ProximaX.Sirius.Storage.SDK.Download
{
    public class DownloadParameterBuilder
    {
        private string TransactionHash { get; }
        private string AccountPrivateKey { get; set; }
        private IPrivacyStrategy PrivacyStrategy { get; set; }
        private bool? ValidateDigest { get; set; }

        public DownloadParameterBuilder(string transactionHash)
        {
            CheckParameter(transactionHash != null, "transactionHash is required");

            TransactionHash = transactionHash;
        }

        public DownloadParameterBuilder WithAccountPrivateKey(string accountPrivateKey)
        {
            CheckParameter(() => accountPrivateKey == null || KeyPair.CreateFromPrivateKey(accountPrivateKey) != null,
                "accountPrivateKey should be a valid private key");

            AccountPrivateKey = accountPrivateKey;
            return this;
        }

        public DownloadParameterBuilder WithValidateDigest(bool? validateDigest)
        {
            ValidateDigest = validateDigest;
            return this;
        }

        public DownloadParameterBuilder WithPrivacyStrategy(IPrivacyStrategy privacyStrategy)
        {
            PrivacyStrategy = privacyStrategy;
            return this;
        }

        public DownloadParameterBuilder WithPlainPrivacy()
        {
            PrivacyStrategy = PlainPrivacyStrategy.Create();
            return this;
        }

        public DownloadParameterBuilder WithNemKeysPrivacy(string privateKey, string publicKey)
        {
            PrivacyStrategy = NemKeysPrivacyStrategy.Create(privateKey, publicKey);
            return this;
        }

        public DownloadParameterBuilder WithPasswordPrivacy(string password)
        {
            PrivacyStrategy = PasswordPrivacyStrategy.Create(password);
            return this;
        }

        public DownloadParameter Build()
        {
            return new DownloadParameter(
                TransactionHash,
                AccountPrivateKey,
                PrivacyStrategy ?? PlainPrivacyStrategy.Create(),
                ValidateDigest ?? false);
        }
    }
}