using System;
using System.IO;
using io.nem2.sdk.Model.Accounts;
using IO.Proximax.SDK.Ciphers;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

// TODO
namespace IO.Proximax.SDK.PrivacyStrategies
{
    public sealed class SecuredWithNemKeysPrivacyStrategy : IPrivacyStrategy
    {
        private BlockchainKeysCipherEncryptor BlockchainKeysCipherEncryptor { get; set; }
        private KeyPair KeyPairOfPrivateKey { get; set; }
        private string PublicKey { get; set; }

        internal SecuredWithNemKeysPrivacyStrategy(BlockchainKeysCipherEncryptor blockchainKeysCipherEncryptor,
            string privateKey, string publicKey) {
            CheckParameter(privateKey != null, "private key is required");
            CheckParameter(publicKey != null, "public key is required");
            
            BlockchainKeysCipherEncryptor = blockchainKeysCipherEncryptor;
            KeyPairOfPrivateKey = KeyPair.CreateFromPrivateKey(privateKey);
            PublicKey = publicKey;
        }

        public override int GetPrivacyType() => (int) Models.PrivacyType.NemKeys;

        public override Stream DecryptStream(Stream data)
        {
            throw new NotImplementedException();
        }

        public override Stream EncryptStream(Stream data)
        {
            throw new NotImplementedException();
        }
        
        public static SecuredWithNemKeysPrivacyStrategy Create(string privateKey, string publicKey) {
            return new SecuredWithNemKeysPrivacyStrategy(new BlockchainKeysCipherEncryptor(), privateKey, publicKey);
        }
    }
}
