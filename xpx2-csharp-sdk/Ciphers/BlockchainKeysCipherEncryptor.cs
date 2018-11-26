using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using IO.Proximax.SDK.Utils;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Security;

namespace IO.Proximax.SDK.Ciphers
{
    public class BlockchainKeysCipherEncryptor
    {

        public Stream EncryptStream(Stream byteStream, string privateKey, string publicKey)
        {
            
            var random = new SecureRandom();

            var salt = new byte[32];
            random.NextBytes(salt);

            var iv = new byte[16];
            random.NextBytes(iv);
            
            var key = GetSecretKey(salt, privateKey, publicKey);
            var aes = GetCipherAes(key, iv);

            return
                new ConcatenatedStream(
                    new List<Stream>()
                    {
                        new MemoryStream(salt),
                        new MemoryStream(iv),
                        new CryptoStream(byteStream, aes.CreateEncryptor(), CryptoStreamMode.Read)    
                    });
            
        }
        
        public Stream DecryptStream(Stream byteStream, string privateKey, string publicKey)
        {
            var salt = byteStream.ReadExactly(32);
            var iv = byteStream.ReadExactly(16);
            
            var key = GetSecretKey(salt, privateKey, publicKey);
            var aes = GetCipherAes(key, iv);

            return new CryptoStream(byteStream, aes.CreateDecryptor(), CryptoStreamMode.Read);
        }
        
        private static Aes GetCipherAes(byte[] key, byte[] iv)
        {
            var aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            return aes;
        }
        
        /*
         * Copied from nem2-sdk Ed25519.derive_key
         */
        private static byte[] GetSecretKey(byte[] salt, string privateKey, string publicKey)
        {
            var shared = new byte[32];
            var longKeyHash = new byte[64];
            var shortKeyHash = new byte[32];

//            Array.Reverse(secretKey);

            // compute  Sha3(512) hash of secret key (as in prepareForScalarMultiply)
            var digestSha3 = new Sha3Digest(512);
            digestSha3.BlockUpdate(privateKey.FromHex(), 0, 32);
            digestSha3.DoFinal(longKeyHash, 0);

            longKeyHash[0] &= 248;
            longKeyHash[31] &= 127;
            longKeyHash[31] |= 64;

            Array.Copy(longKeyHash, 0, shortKeyHash, 0, 32);

            ScalarOperationClamp(shortKeyHash, 0);

            var p = new[] { new long[16], new long[16], new long[16], new long[16] };
            var q = new[] { new long[16], new long[16], new long[16], new long[16] };

            TweetNaCl.Unpackneg(q, publicKey.FromHex()); // returning -1 invalid signature
            TweetNaCl.Scalarmult(p, q, shortKeyHash, 0);
            TweetNaCl.Pack(shared, p);

            // for some reason the most significant bit of the last byte needs to be flipped.
            // doesnt seem to be any corrosponding action in nano/nem.core, so this may be an issue in one of the above 3 functions. i have no idea.
            shared[31] ^= (1 << 7);

            // salt
            for (var i = 0; i < salt.Length; i++)
            {
                shared[i] ^= salt[i];
            }

            // hash salted shared key
            var digestSha3Two = new Sha3Digest(256);
            digestSha3Two.BlockUpdate(shared, 0, 32);
            digestSha3Two.DoFinal(shared, 0);

            return shared;
        }
        
        private static void ScalarOperationClamp(byte[] s, int offset)
        {
            s[offset + 0] &= 248;
            s[offset + 31] &= 127;
            s[offset + 31] |= 64;
        }
    }
    
}