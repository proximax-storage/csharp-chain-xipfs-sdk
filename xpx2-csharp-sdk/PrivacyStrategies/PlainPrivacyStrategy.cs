using System.IO;

namespace IO.Proximax.SDK.PrivacyStrategies
{
    public sealed class PlainPrivacyStrategy: IPrivacyStrategy
    {
        private PlainPrivacyStrategy()
        {
            
        }

        public override int GetPrivacyType() => (int) Models.PrivacyType.Plain;

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
