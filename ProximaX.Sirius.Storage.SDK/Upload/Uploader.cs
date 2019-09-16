using System;
using System.Reactive.Linq;
using ProximaX.Sirius.Storage.SDK.Async;
using ProximaX.Sirius.Storage.SDK.Connections;
using ProximaX.Sirius.Storage.SDK.Exceptions;
using ProximaX.Sirius.Storage.SDK.Models;
using ProximaX.Sirius.Storage.SDK.Services;
using ProximaX.Sirius.Storage.SDK.Utils;
using static ProximaX.Sirius.Storage.SDK.Utils.ParameterValidationUtils;

namespace ProximaX.Sirius.Storage.SDK.Upload
{
    public class Uploader
    {
        private BlockchainTransactionService BlockchainTransactionService { get; }
        private CreateProximaxDataService CreateProximaxDataService { get; }
        private CreateProximaxMessagePayloadService CreateProximaxMessagePayloadService { get; }

        public Uploader(ConnectionConfig connectionConfig)
        {
            CreateProximaxDataService = new CreateProximaxDataService(connectionConfig.FileStorageConnection);
            CreateProximaxMessagePayloadService = new CreateProximaxMessagePayloadService();
            BlockchainTransactionService =
                new BlockchainTransactionService(connectionConfig.BlockchainNetworkConnection);
        }

        internal Uploader(BlockchainTransactionService blockchainTransactionService,
            CreateProximaxDataService proximaxDataService,
            CreateProximaxMessagePayloadService proximaxMessagePayloadService)
        {
            BlockchainTransactionService = blockchainTransactionService;
            CreateProximaxDataService = proximaxDataService;
            CreateProximaxMessagePayloadService = proximaxMessagePayloadService;
        }

        public UploadResult Upload(UploadParameter uploadParam)
        {
            CheckParameter(uploadParam != null, "uploadParam is required");

            return DoUpload(uploadParam).Wait();
        }

        public AsyncTask UploadAsync(UploadParameter uploadParam, AsyncCallbacks<UploadResult> asyncCallbacks)
        {
            CheckParameter(uploadParam != null, "uploadParam is required");

            var asyncTask = new AsyncTask();

            AsyncUtils.ProcessFirstItem(DoUpload(uploadParam), asyncCallbacks, asyncTask);

            return asyncTask;
        }

        private IObservable<UploadResult> DoUpload(UploadParameter uploadParam)
        {
            try
            {
                var uploadResult = CreateProximaxDataService.CreateData(uploadParam).SelectMany(uploadedData =>
                    CreateProximaxMessagePayloadService.CreateMessagePayload(uploadParam, uploadedData)
                        .SelectMany(messagePayload =>
                            CreateAndAnnounceTransaction(uploadParam, messagePayload)
                                .Select(transactionHash =>
                                    CreateUploadResult(messagePayload, transactionHash)))).Wait();
                return Observable.Return(uploadResult);
            }
            catch (Exception ex)
            {
                return Observable.Throw<UploadResult>(new UploadFailureException("Upload failed.", ex));
            }
        }

        private IObservable<string> CreateAndAnnounceTransaction(UploadParameter uploadParam,
            ProximaxMessagePayloadModel messagePayload)
        {
            return BlockchainTransactionService.CreateAndAnnounceTransaction(
                messagePayload, uploadParam.SignerPrivateKey, uploadParam.RecipientPublicKey,
                uploadParam.RecipientAddress,
                uploadParam.TransactionDeadline, uploadParam.TransactionMosaics,
                uploadParam.UseBlockchainSecureMessage);
        }

        private UploadResult CreateUploadResult(ProximaxMessagePayloadModel messagePayload, string transactionHash)
        {
            return UploadResult.Create(transactionHash, messagePayload.PrivacyType, messagePayload.Version,
                messagePayload.Data);
        }
    }
}