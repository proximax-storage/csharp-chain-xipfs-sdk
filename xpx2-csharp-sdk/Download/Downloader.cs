using System;
using System.IO;
using System.Reactive.Linq;
using io.nem2.sdk.Model.Transactions;
using IO.Proximax.SDK.Async;
using IO.Proximax.SDK.Connections;
using IO.Proximax.SDK.Exceptions;
using IO.Proximax.SDK.Models;
using IO.Proximax.SDK.PrivacyStrategies;
using IO.Proximax.SDK.Services;
using IO.Proximax.SDK.Utils;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

// TODO 
namespace IO.Proximax.SDK.Download
{
    public class Downloader
    {
        private BlockchainTransactionService BlockchainTransactionService { get; }
        private RetrieveProximaxMessagePayloadService RetrieveProximaxMessagePayloadService { get; }
        private RetrieveProximaxDataService RetrieveProximaxDataService { get; }

        public Downloader(ConnectionConfig connectionConfig)
        {
            RetrieveProximaxMessagePayloadService =
                new RetrieveProximaxMessagePayloadService(connectionConfig.BlockchainNetworkConnection);
            RetrieveProximaxDataService = new RetrieveProximaxDataService(connectionConfig.FileStorageConnection);
            BlockchainTransactionService =
                new BlockchainTransactionService(connectionConfig.BlockchainNetworkConnection);
        }

        // constructor for unit testing purposes
        internal Downloader(BlockchainTransactionService blockchainTransactionService,
            RetrieveProximaxMessagePayloadService retrieveProximaxMessagePayloadService,
            RetrieveProximaxDataService retrieveProximaxDataService)
        {
            BlockchainTransactionService = blockchainTransactionService;
            RetrieveProximaxMessagePayloadService = retrieveProximaxMessagePayloadService;
            RetrieveProximaxDataService = retrieveProximaxDataService;
        }

        public DownloadResult Download(DownloadParameter downloadParam)
        {
            CheckParameter(downloadParam != null, "downloadParam is required");

            return DoCompleteDownload(downloadParam).Wait();
        }

        public AsyncTask DownloadAsync(DownloadParameter downloadParam, AsyncCallbacks<DownloadResult> asyncCallbacks)
        {
            CheckParameter(downloadParam != null, "downloadParam is required");

            var asyncTask = new AsyncTask();

            AsyncUtils.ProcessFirstItem(DoCompleteDownload(downloadParam), asyncCallbacks, asyncTask);

            return asyncTask;
        }

        public Stream DirectDownload(DirectDownloadParameter directDownloadParameter)
        {
            CheckParameter(directDownloadParameter != null, "directDownloadParameter is required");

            return DoDirectDownload(directDownloadParameter).Wait();
        }

        public AsyncTask DirectDownloadAsync(DirectDownloadParameter directDownloadParameter,
            AsyncCallbacks<Stream> asyncCallbacks)
        {
            CheckParameter(directDownloadParameter != null, "directDownloadParameter is required");

            var asyncTask = new AsyncTask();

            AsyncUtils.ProcessFirstItem(DoDirectDownload(directDownloadParameter), asyncCallbacks, asyncTask);

            return asyncTask;
        }

        private IObservable<DownloadResult> DoCompleteDownload(DownloadParameter downloadParam)
        {
            try
            {
                var result = BlockchainTransactionService.GetTransferTransaction(downloadParam.TransactionHash)
                    .Select(transferTransaction => RetrieveProximaxMessagePayloadService.GetMessagePayload(
                        transferTransaction,
                        downloadParam.AccountPrivateKey))
                    .Select(messagePayload => CreateCompleteDownloadResult(messagePayload,
                        () => GetDataByteStream(messagePayload, null, downloadParam.PrivacyStrategy,
                            downloadParam.ValidateDigest, null).Wait(), downloadParam.TransactionHash)).Wait();
                return Observable.Return(result);
            }
            catch (Exception ex)
            {
                return Observable.Throw<DownloadResult>(new DownloadFailureException("Download failed.", ex));
            }
        }

        private DownloadResult CreateCompleteDownloadResult(ProximaxMessagePayloadModel messagePayload,
            Func<Stream> byteStreamSupplier, string transactionHash)
        {
            var data = messagePayload.Data;
            return DownloadResult.Create(transactionHash, messagePayload.PrivacyType, messagePayload.Version,
                new DownloadResultData(byteStreamSupplier, data.Digest, data.DataHash, data.Timestamp,
                    data.Description, data.Name, data.ContentType, data.Metadata));
        }

        private IObservable<Stream> DoDirectDownload(DirectDownloadParameter downloadParam)
        {
            try
            {
                var result = GetOptionalBlockchainTransaction(downloadParam.TransactionHash)
                    .Select(transferTransaction =>
                        transferTransaction != null
                            ? RetrieveProximaxMessagePayloadService.GetMessagePayload(transferTransaction,
                                downloadParam.AccountPrivateKey)
                            : null)
                    .SelectMany(messagePayload => GetDataByteStream(messagePayload, downloadParam.DataHash,
                        downloadParam.PrivacyStrategy,
                        downloadParam.ValidateDigest, downloadParam.Digest)).Wait();
                return Observable.Return(result);
            }
            catch (Exception ex)
            {
                return Observable.Throw<Stream>(new DirectDownloadFailureException("Direct download failed.", ex));
            }
        }

        private IObservable<TransferTransaction> GetOptionalBlockchainTransaction(string transactionHash)
        {
            return transactionHash != null
                ? BlockchainTransactionService.GetTransferTransaction(transactionHash)
                : Observable.Return<TransferTransaction>(null);
        }

        private IObservable<Stream> GetDataByteStream(ProximaxMessagePayloadModel messagePayload, string dataHash,
            IPrivacyStrategy privacyStrategy, bool validateDigest, string digest)
        {
            var resolvedDataHash = messagePayload != null ? messagePayload.Data.DataHash : dataHash;
            var resolvedDigest = messagePayload != null ? messagePayload.Data.Digest : digest;
            var resolvedContentType = messagePayload?.Data.ContentType;

            return RetrieveProximaxDataService.GetDataByteStream(resolvedDataHash, privacyStrategy, validateDigest,
                resolvedDigest, resolvedContentType);
        }
    }
}