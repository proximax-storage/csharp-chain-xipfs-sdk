using io.nem2.sdk.Model.Transactions.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

namespace IO.Proximax.SDK.PrivacyStrategies
{
    public abstract class IPrivacyStrategy
    {
        public string PrivacySearchTag { get; }
        public abstract int PrivacyType();
        public abstract byte[] EncryptData(byte[] data);
        public abstract byte[] DecryptData(byte[] data);
        public abstract IMessage EncodePayloadAsMessage(string payload);
        public abstract string DecodePayloadFromMessage(IMessage message);

        internal IPrivacyStrategy(string privacySearchTag)
        {
            CheckParameter(privacySearchTag == null || privacySearchTag.Length <= 50,
                "privacy search tag cannot be more than 50 characters");

            PrivacySearchTag = privacySearchTag;
        }
    }
}
