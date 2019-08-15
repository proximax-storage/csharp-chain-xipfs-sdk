using System;
using ProximaX.Sirius.Storage.SDK.Connections;
using ProximaX.Sirius.Storage.SDK.Services.Clients;
using ProximaX.Sirius.Storage.SDK.Services.Repositories;
using static ProximaX.Sirius.Storage.SDK.Utils.ParameterValidationUtils;

namespace ProximaX.Sirius.Storage.SDK.Services.Factories
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