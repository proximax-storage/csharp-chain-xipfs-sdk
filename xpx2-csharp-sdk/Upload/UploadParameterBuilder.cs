using io.nem2.sdk.Model.Accounts;
using IO.Proximax.SDK.PrivacyStrategies;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

namespace IO.Proximax.SDK.Upload
{
    public class UploadParameterBuilder
    {
        private IUploadParameterData Data { get; }
        private string SignerPrivateKey { get; }
        private string RecipientPublicKey { get; set; }
        private string RecipientAddress { get; set; }
        private bool? ComputeDigest { get; set; }
        private bool? DetectContentType { get; set; }
        private int? TransactionDeadline { get; set; }
        private bool? UseBlockchainSecureMessage { get; set; }
        private IPrivacyStrategy PrivacyStrategy { get; set; }

        public UploadParameterBuilder(IUploadParameterData data, string signerPrivateKey)
        {
            CheckParameter(data != null, "data is required");
            CheckParameter(signerPrivateKey != null, "signerPrivateKey is required");
            CheckParameter(() => KeyPair.CreateFromPrivateKey(signerPrivateKey) != null,
                "signerPrivateKey should be a valid private key");

            Data = data;
            SignerPrivateKey = signerPrivateKey;
        }


        public UploadParameterBuilder WithRecipientPublicKey(string recipientPublicKey)
        {
            RecipientPublicKey = recipientPublicKey;
            return this;
        }

        public UploadParameterBuilder WithRecipientAddress(string recipientAddress)
        {
            RecipientAddress = recipientAddress;
            return this;
        }

        public UploadParameterBuilder WithComputeDigest(bool? computeDigest)
        {
            ComputeDigest = computeDigest;
            return this;
        }

        public UploadParameterBuilder WithDetectContentType(bool? detectContentType)
        {
            DetectContentType = detectContentType;
            return this;
        }


        public UploadParameterBuilder WithUseBlockchainSecureMessage(bool? useBlockchainSecureMessage)
        {
            UseBlockchainSecureMessage = useBlockchainSecureMessage;
            return this;
        }

        public UploadParameterBuilder WithTransactionDeadline(int? transactionDeadline)
        {
            CheckParameter(transactionDeadline == null || (transactionDeadline >= 1 && transactionDeadline <= 23),
                "transactionDeadline should be between 1 and 23");

            TransactionDeadline = transactionDeadline;
            return this;
        }

        public UploadParameterBuilder WithPrivacyStrategy(IPrivacyStrategy privacyStrategy)
        {
            PrivacyStrategy = privacyStrategy;
            return this;
        }

        public UploadParameterBuilder WithPlainPrivacy()
        {
            PrivacyStrategy = PlainPrivacyStrategy.Create();
            return this;
        }
        
        public UploadParameterBuilder WithNemKeysPrivacy(string privateKey, string publicKey) {
            PrivacyStrategy = SecuredWithNemKeysPrivacyStrategy.Create(privateKey, publicKey);
            return this;
        }

        public UploadParameterBuilder WithPasswordPrivacy(string password) {
            PrivacyStrategy = SecuredWithPasswordPrivacyStrategy.Create(password);
            return this;
        }

        public UploadParameter Build()
        {
            return new UploadParameter(Data, SignerPrivateKey, RecipientPublicKey, RecipientAddress, 
                ComputeDigest ?? false, 
                DetectContentType ?? false,
                TransactionDeadline ?? 12, 
                UseBlockchainSecureMessage ?? false, 
                PrivacyStrategy ?? PlainPrivacyStrategy.Create());        
        }
    }
}
