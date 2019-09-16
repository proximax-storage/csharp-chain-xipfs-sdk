using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using ProximaX.Sirius.Storage.SDK.Connections;
using ProximaX.Sirius.Storage.SDK.Services.Repositories;
using static ProximaX.Sirius.Storage.SDK.Utils.ParameterValidationUtils;

namespace ProximaX.Sirius.Storage.SDK.Services.Clients
{
    public class IpfsApiClient : IFileRepository
    {
        private IpfsConnection IpfsConnection { get; set; }

        public IpfsApiClient(IpfsConnection ipfsConnection)
        {
            IpfsConnection = ipfsConnection;
        }

        public override IObservable<string> AddByteStream(Stream byteStream)
        {
            CheckParameter(byteStream != null, "byteStream is required");

            using (byteStream)
            {
                var fileSystemNode = IpfsConnection.Ipfs.FileSystem.AddAsync(byteStream)
                    .GetAwaiter()
                    .GetResult();
                return Observable.Return(fileSystemNode.Id.Hash.ToBase58());
            }
        }

        public override IObservable<string> AddPath(string path)
        {
            CheckParameter(path != null, "path is required");
            CheckParameter(File.GetAttributes(path).HasFlag(FileAttributes.Directory),
                "path should be directory/folder");

            return IpfsConnection.Ipfs.FileSystem.AddDirectoryAsync(path).ToObservable()
                .Select(node => node.Id.Hash.ToBase58());
        }

        public IObservable<IEnumerable<string>> Pin(string dataHash)
        {
            CheckParameter(dataHash != null, "dataHash is required");

            return IpfsConnection.Ipfs.Pin.AddAsync(dataHash).ToObservable()
                .Select(enumerable => enumerable.Select(cid => cid.Hash.ToBase58()));
        }

        public override IObservable<Stream> GetByteStream(string dataHash)
        {
            CheckParameter(dataHash != null, "dataHash is required");

            return IpfsConnection.Ipfs.FileSystem.ReadFileAsync(dataHash).ToObservable();
        }
    }
}