using io.nem2.sdk.Model.Transactions.Messages;
using IO.Proximax.SDK.Models;
using IO.Proximax.SDK.PrivacyStrategies;
using IO.Proximax.SDK.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

namespace IO.Proximax.SDK.Services.Factories
{
    public class BlockchainMessageFactory
    {

        public IMessage CreateMessage(IPrivacyStrategy privacyStrategy, ProximaxMessagePayloadModel messagePayload)
        {
            CheckParameter(privacyStrategy != null, "privacyStrategy is required");
            CheckParameter(messagePayload != null, "messagePayload is required");

            String jsonPayload = JsonUtils.ToJson(messagePayload);
            return privacyStrategy.EncodePayloadAsMessage(jsonPayload);
        }
    }
}
