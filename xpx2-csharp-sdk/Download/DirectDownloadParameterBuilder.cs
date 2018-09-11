using io.nem2.sdk.Model.Accounts;
using IO.Proximax.SDK.PrivacyStrategies;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

namespace IO.Proximax.SDK.Download
{
    public class DirectDownloadParameterBuilder
    {
        private string TransactionHash { get; set; }
        private string AccountPrivateKey { get; set; }
        private string DataHash { get; set; }
        private bool ValidateDigest{ get; set; }
        private IPrivacyStrategy PrivacyStrategy { get; set; }
        private string Digest { get; set; }

        private DirectDownloadParameterBuilder()
        {            
        }
        
        public static DirectDownloadParameterBuilder CreateFromTransactionHash(string transactionHash, 
            string accountPrivateKey, bool? validateDigest)
        {
            CheckParameter(transactionHash != null, "transactionHash is required");
            CheckParameter(() => accountPrivateKey == null || KeyPair.CreateFromPrivateKey(accountPrivateKey) != null,
            "accountPrivateKey should be a valid private key");

            var builder = new DirectDownloadParameterBuilder
            {
                TransactionHash = transactionHash,
                AccountPrivateKey = accountPrivateKey,
                ValidateDigest = validateDigest ?? false
            };
            return builder;            
        }

        public static DirectDownloadParameterBuilder CreateFromDataHash(string dataHash, string digest) {
            CheckParameter(dataHash != null, "dataHash is required");
            //CheckParameter(() => Multihash.fromBase58(dataHash) != null, "dataHash should be a valid ipfs hash");

            var builder = new DirectDownloadParameterBuilder
            {
                DataHash = dataHash, 
                Digest = digest, 
                ValidateDigest = true
            };
            return builder;
        }
        
        public DirectDownloadParameterBuilder WithPrivacyStrategy(IPrivacyStrategy privacyStrategy)
        {
            PrivacyStrategy = privacyStrategy;
            return this;
        }

        public DirectDownloadParameterBuilder WithPlainPrivacy()
        {
            PrivacyStrategy = PlainPrivacyStrategy.Create();
            return this;
        }
        
        public DirectDownloadParameterBuilder WithNemKeysPrivacy(string privateKey, string publicKey) {
            PrivacyStrategy = SecuredWithNemKeysPrivacyStrategy.Create(privateKey, publicKey);
            return this;
        }

        public DirectDownloadParameterBuilder WithPasswordPrivacy(string password) {
            PrivacyStrategy = SecuredWithPasswordPrivacyStrategy.Create(password);
            return this;
        }

        public DirectDownloadParameter Build()
        {
            return new DirectDownloadParameter(TransactionHash, AccountPrivateKey, DataHash, ValidateDigest, 
                PrivacyStrategy ?? PlainPrivacyStrategy.Create(), Digest);
        }

    }
}
