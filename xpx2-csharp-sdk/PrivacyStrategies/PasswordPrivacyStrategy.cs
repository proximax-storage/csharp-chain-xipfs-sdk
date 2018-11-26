using System;
using System.IO;
using IO.Proximax.SDK.Ciphers;
using IO.Proximax.SDK.Utils;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

// TODO
namespace IO.Proximax.SDK.PrivacyStrategies
{
    public sealed class PasswordPrivacyStrategy : IPrivacyStrategy
    {
        public const int MinimumPasswordLength = 50;

        private PBECipherEncryptor PbeCipherEncryptor { get; }        
        public string Password { get; set; }

        internal PasswordPrivacyStrategy(PBECipherEncryptor pbeCipherEncryptor, string password) {
            CheckParameter(password != null, "password is required");
            CheckParameter(password.Length >= MinimumPasswordLength, "minimum length for password is 50");

            PbeCipherEncryptor = pbeCipherEncryptor;
            Password = password;
        }

        public override int GetPrivacyType() => (int) Models.PrivacyType.Password;

        public override Stream DecryptStream(Stream data)
        {
            return PbeCipherEncryptor.DecryptStream(data, Password);
        }

        public override Stream EncryptStream(Stream data)
        {
            return PbeCipherEncryptor.EncryptStream(data, Password);
        }
        
        public static PasswordPrivacyStrategy Create(string password) {
            return new PasswordPrivacyStrategy(new PBECipherEncryptor(), password);
        }
        
        public static PasswordPrivacyStrategy Create() {
            return new PasswordPrivacyStrategy(new PBECipherEncryptor(),
                PasswordUtils.GeneratePassword());
        }
    }
}