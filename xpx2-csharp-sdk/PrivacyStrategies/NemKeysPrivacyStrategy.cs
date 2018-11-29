using System.IO;
using IO.Proximax.SDK.Ciphers;
using IO.Proximax.SDK.Models;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

namespace IO.Proximax.SDK.PrivacyStrategies
{
    public sealed class NemKeysPrivacyStrategy : IPrivacyStrategy
    {
        private BlockchainKeysCipherEncryptor BlockchainKeysCipherEncryptor { get; set; }
        private string PrivateKey { get; set; }
        private string PublicKey { get; set; }

        internal NemKeysPrivacyStrategy(BlockchainKeysCipherEncryptor blockchainKeysCipherEncryptor,
            string privateKey, string publicKey)
        {
            CheckParameter(privateKey != null, "private key is required");
            CheckParameter(publicKey != null, "public key is required");

            BlockchainKeysCipherEncryptor = blockchainKeysCipherEncryptor;
            PrivateKey = privateKey;
            PublicKey = publicKey;
        }

        public override int GetPrivacyType() => (int) PrivacyType.NemKeys;

        public override Stream DecryptStream(Stream data)
        {
            return BlockchainKeysCipherEncryptor.DecryptStream(data, PrivateKey, PublicKey);
        }

        public override Stream EncryptStream(Stream data)
        {
            return BlockchainKeysCipherEncryptor.EncryptStream(data, PrivateKey, PublicKey);
        }

        public static NemKeysPrivacyStrategy Create(string privateKey, string publicKey)
        {
            return new NemKeysPrivacyStrategy(new BlockchainKeysCipherEncryptor(), privateKey, publicKey);
        }
    }
}