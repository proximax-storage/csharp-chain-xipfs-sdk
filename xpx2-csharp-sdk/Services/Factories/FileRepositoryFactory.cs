using System;
using IO.Proximax.SDK.Connections;
using IO.Proximax.SDK.Services.Clients;
using IO.Proximax.SDK.Services.Repositories;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

namespace IO.Proximax.SDK.Services.Factories
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