using System;
using System.Collections.Generic;
using System.Text;
using IO.Proximax.SDK.Models;

namespace IO.Proximax.SDK.PrivacyStrategies
{
    public sealed class PlainPrivacyStrategy: IPlainMessagePrivacyStrategy
    {
        PlainPrivacyStrategy(string searchTag): base(searchTag) { }

        public override int PrivacyType() => (int) Models.PrivacyType.Types.PLAIN;

        public override byte[] DecryptData(byte[] data)
        {
            return data;
        }

        public override byte[] EncryptData(byte[] data)
        {
            return data;
        }

        public static PlainPrivacyStrategy Create(string searchTag)
        {
            return new PlainPrivacyStrategy(searchTag);
        }

    }
}
