using System;
using System.IO;
using System.Reactive.Linq;
using IO.Proximax.SDK.Connections;
using IO.Proximax.SDK.PrivacyStrategies;
using IO.Proximax.SDK.Services.Factories;
using IO.Proximax.SDK.Services.Repositories;
using IO.Proximax.SDK.Utils;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

namespace IO.Proximax.SDK.Services
{
    public class FileDownloadService
    {
        private IFileRepository FileRepository { get; }
        private DigestUtils DigestUtils { get; }

        public FileDownloadService(IFileStorageConnection fileStorageConnection)
        {
            FileRepository = FileRepositoryFactory.Create(fileStorageConnection);
            DigestUtils = new DigestUtils();
        }

        internal FileDownloadService(IFileRepository fileRepository, DigestUtils digestUtils)
        {
            FileRepository = fileRepository;
            DigestUtils = digestUtils;
        }

        public IObservable<Stream> GetByteStream(string dataHash, IPrivacyStrategy privacyStrategy, string digest) {
            
            CheckParameter(dataHash != null, "dataHash is required");

            var privacyStrategyToUse = privacyStrategy ?? PlainPrivacyStrategy.Create();

            ValidateDigest(digest, dataHash);

            return FileRepository.GetByteStream(dataHash).Select(stream => privacyStrategyToUse.DecryptStream(stream));
        }
        
        private void ValidateDigest(string digest, string dataHash) {
            if (digest != null) {
                FileRepository.GetByteStream(dataHash)
                    .SelectMany(undecryptedStream => DigestUtils.ValidateDigest(undecryptedStream, digest))
                    .Wait();
            }
        }
    }
}
