using io.nem2.sdk.Model.Transactions.Messages;
using IO.Proximax.SDK.Models;
using IO.Proximax.SDK.Utils;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

namespace IO.Proximax.SDK.Services.Factories
{
    public class BlockchainMessageFactory
    {

        public IMessage CreateMessage(ProximaxMessagePayloadModel messagePayload, string signerPrivateKey, string recipientPublicKey,
            string recipientAddress, bool useBlockchainSecureMessage)
        {
            CheckParameter(messagePayload != null, "messagePayload is required");

            // TODO handle secure message
            var jsonPayload = JsonUtils.ToJson(messagePayload);
            return PlainMessage.Create(jsonPayload);
        }
    }
}
