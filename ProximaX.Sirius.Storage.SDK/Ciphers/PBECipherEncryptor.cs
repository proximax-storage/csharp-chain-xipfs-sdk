using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using ProximaX.Sirius.Storage.SDK.Utils;
using Org.BouncyCastle.Security;

namespace ProximaX.Sirius.Storage.SDK.Ciphers
{
    public class PBECipherEncryptor
    {
        public Stream EncryptStream(Stream byteStream, string password)
        {
            var random = new SecureRandom();

            var salt = new byte[32];
            random.NextBytes(salt);

            var iv = new byte[16];
            random.NextBytes(iv);

            var key = GetSecretKey(password, salt);
            var aes = GetCipherAes(key, iv);

            return new ConcatenatedStream(
                new List<Stream>()
                {
                    new MemoryStream(salt),
                    new MemoryStream(iv),
                    new CryptoStream(byteStream, aes.CreateEncryptor(), CryptoStreamMode.Read)
                });
        }

        public Stream DecryptStream(Stream byteStream, string password)
        {
            var salt = byteStream.ReadExactly(32);
            var iv = byteStream.ReadExactly(16);

            var key = GetSecretKey(password, salt);
            var aes = GetCipherAes(key, iv);

            return new CryptoStream(byteStream, aes.CreateDecryptor(), CryptoStreamMode.Read);
        }

        private static byte[] GetSecretKey(string password, byte[] salt)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt);
            pbkdf2.IterationCount = 65536;
            var key = pbkdf2.GetBytes(256 / 8);
            return key;
        }

        private static Aes GetCipherAes(byte[] key, byte[] iv)
        {
            var aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            return aes;
        }
    }
}