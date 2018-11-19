using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using IO.Proximax.SDK.Connections;
using IO.Proximax.SDK.Services.Repositories;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

namespace IO.Proximax.SDK.Services.Clients
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

            return IpfsConnection.Ipfs.FileSystem.AddAsync(byteStream).ToObservable()
                .Select(node => node.Id.Hash.ToBase58());
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