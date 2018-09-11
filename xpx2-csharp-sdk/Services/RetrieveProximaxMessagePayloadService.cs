using io.nem2.sdk.Model.Transactions;
using io.nem2.sdk.Model.Transactions.Messages;
using IO.Proximax.SDK.Exceptions;
using IO.Proximax.SDK.Models;
using IO.Proximax.SDK.Utils;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

namespace IO.Proximax.SDK.Services
{
    public class RetrieveProximaxMessagePayloadService
    {
        public ProximaxMessagePayloadModel GetMessagePayload(TransferTransaction transferTransaction, string accountPrivateKey) {
            CheckParameter(transferTransaction != null, "transferTransaction is required");

            // TODO handle secure message
            switch (transferTransaction.Message)
            {
                    case PlainMessage plainMessage:
                        var messagePayload = plainMessage.GetStringPayload();
                        return JsonUtils.FromJson<ProximaxMessagePayloadModel>(messagePayload);
                    default:
                        throw new DownloadForMessageTypeNotSupportedException(
                            $"Download of message type {transferTransaction.Message.GetType()} is not supported");
            }
        }

    }
}
