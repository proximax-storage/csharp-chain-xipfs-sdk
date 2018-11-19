using io.nem2.sdk.Model.Transactions;
using IO.Proximax.SDK.Connections;
using IO.Proximax.SDK.Models;
using IO.Proximax.SDK.Utils;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

namespace IO.Proximax.SDK.Services
{
    public class RetrieveProximaxMessagePayloadService
    {
        private BlockchainMessageService BlockchainMessageService { get; }

        public RetrieveProximaxMessagePayloadService(BlockchainNetworkConnection blockchainNetworkConnection)
        {
            BlockchainMessageService = new BlockchainMessageService(blockchainNetworkConnection);
        }

        internal RetrieveProximaxMessagePayloadService(BlockchainMessageService blockchainMessageService)
        {
            BlockchainMessageService = blockchainMessageService;
        }

        public ProximaxMessagePayloadModel GetMessagePayload(TransferTransaction transferTransaction,
            string accountPrivateKey)
        {
            CheckParameter(transferTransaction != null, "transferTransaction is required");

            var payload = BlockchainMessageService.GetMessagePayload(transferTransaction, accountPrivateKey);
            return JsonUtils.FromJson<ProximaxMessagePayloadModel>(payload);
        }
    }
}