using System;
using System.IO;
using System.Reactive.Linq;
using Proximax.Storage.SDK.Connections;
using Proximax.Storage.SDK.PrivacyStrategies;
using Proximax.Storage.SDK.Services.Factories;
using Proximax.Storage.SDK.Services.Repositories;
using Proximax.Storage.SDK.Utils;
using static Proximax.Storage.SDK.Utils.ParameterValidationUtils;

namespace Proximax.Storage.SDK.Services
{
    public class FileDownloadService
    {
        private IFileRepository FileRepository { get; }

        public FileDownloadService(IFileStorageConnection fileStorageConnection)
        {
            FileRepository = FileRepositoryFactory.Create(fileStorageConnection);
        }

        internal FileDownloadService(IFileRepository fileRepository)
        {
            FileRepository = fileRepository;
        }

        public IObservable<Stream> GetByteStream(string dataHash, IPrivacyStrategy privacyStrategy, string digest)
        {
            CheckParameter(dataHash != null, "dataHash is required");

            var privacyStrategyToUse = privacyStrategy ?? PlainPrivacyStrategy.Create();

            ValidateDigest(digest, dataHash);

            return FileRepository.GetByteStream(dataHash).Select(stream => privacyStrategyToUse.DecryptStream(stream));
        }

        private void ValidateDigest(string digest, string dataHash)
        {
            if (digest != null)
            {
                FileRepository.GetByteStream(dataHash)
                    .Select(undecryptedStream => undecryptedStream.ValidateDigest(digest))
                    .Wait();
            }
        }
    }
}