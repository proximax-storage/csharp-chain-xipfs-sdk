using System.IO;
using Proximax.Storage.SDK.Models;

namespace Proximax.Storage.SDK.PrivacyStrategies
{
    public sealed class PlainPrivacyStrategy : IPrivacyStrategy
    {
        private PlainPrivacyStrategy()
        {
        }

        public override int GetPrivacyType() => (int) PrivacyType.Plain;

        public override Stream DecryptStream(Stream data)
        {
            return data;
        }

        public override Stream EncryptStream(Stream data)
        {
            return data;
        }

        public static PlainPrivacyStrategy Create()
        {
            return new PlainPrivacyStrategy();
        }
    }
}