using System;
using Proximax.Storage.SDK.Connections;
using Proximax.Storage.SDK.Services.Clients;
using Proximax.Storage.SDK.Services.Repositories;
using static Proximax.Storage.SDK.Utils.ParameterValidationUtils;

namespace Proximax.Storage.SDK.Services.Factories
{
    public class FileRepositoryFactory
    {
        public static IFileRepository Create(IFileStorageConnection fileStorageConnection)
        {
            CheckParameter(fileStorageConnection != null, "fileStorageConnection is required");

            switch (fileStorageConnection)
            {
                case IpfsConnection ipfsConnection:
                    return new IpfsApiClient(ipfsConnection);
                case StorageConnection storageConnection:
                    return new StorageNodeClient(storageConnection);
                default:
                    throw new NotSupportedException($"Unknown file storage connection {fileStorageConnection}");
            }
        }
    }
}