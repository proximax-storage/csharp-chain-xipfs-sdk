
using ProximaX.Sirius.Storage.SDK.Connections;
using ProximaX.Sirius.Storage.SDK.Models;
using ProximaX.Sirius.Storage.SDK.Utils;
using ProximaX.Sirius.Chain.Sdk.Model.Transactions;
using static ProximaX.Sirius.Storage.SDK.Utils.ParameterValidationUtils;

namespace ProximaX.Sirius.Storage.SDK.Services
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