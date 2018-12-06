using io.nem2.sdk.Model.Transactions;
using Proximax.Storage.SDK.Connections;
using Proximax.Storage.SDK.Models;
using Proximax.Storage.SDK.Utils;
using static Proximax.Storage.SDK.Utils.ParameterValidationUtils;

namespace Proximax.Storage.SDK.Services
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

            return BlockchainMessageService.GetMessagePayload(transferTransaction, accountPrivateKey)
                .FromJson<ProximaxMessagePayloadModel>();
        }
    }
}