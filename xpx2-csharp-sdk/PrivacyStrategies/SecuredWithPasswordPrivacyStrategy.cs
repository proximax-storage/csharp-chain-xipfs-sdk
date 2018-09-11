using System;
using System.IO;
using IO.Proximax.SDK.Ciphers;
using IO.Proximax.SDK.Utils;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

// TODO
namespace IO.Proximax.SDK.PrivacyStrategies
{
    public sealed class SecuredWithPasswordPrivacyStrategy : IPrivacyStrategy
    {
        public const int MinimumPasswordLength = 50;

        private PBECipherEncryptor PbeCipherEncryptor { get; }        
        public string Password { get; set; }

        internal SecuredWithPasswordPrivacyStrategy(PBECipherEncryptor pbeCipherEncryptor, string password) {
            CheckParameter(password != null, "password is required");
            CheckParameter(password.Length >= MinimumPasswordLength, "minimum length for password is 50");

            PbeCipherEncryptor = pbeCipherEncryptor;
            Password = password;
        }

        public override int GetPrivacyType() => (int) Models.PrivacyType.Password;

        public override Stream DecryptStream(Stream data)
        {
            throw new NotImplementedException();
        }

        public override Stream EncryptStream(Stream data)
        {
            throw new NotImplementedException();
        }
        
        public static SecuredWithPasswordPrivacyStrategy Create(string password) {
            return new SecuredWithPasswordPrivacyStrategy(new PBECipherEncryptor(), password);
        }
        
        public static SecuredWithPasswordPrivacyStrategy Create() {
            return new SecuredWithPasswordPrivacyStrategy(new PBECipherEncryptor(),
                PasswordUtils.generatePassword());
        }
    }
}