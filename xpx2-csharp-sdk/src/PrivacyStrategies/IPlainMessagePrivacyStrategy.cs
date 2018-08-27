using System;
using System.Collections.Generic;
using System.Text;
using io.nem2.sdk.Model.Transactions.Messages;
using IO.Proximax.SDK.Exceptions;

namespace IO.Proximax.SDK.PrivacyStrategies
{
    public abstract class IPlainMessagePrivacyStrategy : IPrivacyStrategy
    {
        internal IPlainMessagePrivacyStrategy(string privacySearchTag): base(privacySearchTag) { }

        public override sealed string DecodePayloadFromMessage(IMessage message)
        {
            if (message is PlainMessage)
            {
                return (message as PlainMessage).GetStringPayload();
            }
            else throw new DecodeTransactionMessageFailureException("cannot decode a non-plain message");
            
        }

        public override sealed IMessage EncodePayloadAsMessage(string payload)
        {
            return PlainMessage.Create(payload);
        }
    }
}
