using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive.Linq;
using IO.Proximax.SDK.Connections;
using IO.Proximax.SDK.Services.Repositories;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

namespace IO.Proximax.SDK.Services.Clients
{
    public class StorageNodeClient : IFileRepository
    {
        private StorageConnection StorageConnection { get; set; }

        public StorageNodeClient(StorageConnection storageConnection)
        {
            StorageConnection = storageConnection;
        }

        public override IObservable<string> AddByteStream(Stream byteStream)
        {
            CheckParameter(byteStream != null, "byteStream is required");

            using (byteStream)
            {
                // TODO
                throw new NotImplementedException();
            }
        }

        public override IObservable<string> AddPath(string path)
        {
            return Observable.Throw<string>(new NotSupportedException("Path upload is not supported on storage"));
        }

        public IObservable<IEnumerable<string>> Pin(string dataHash)
        {
            CheckParameter(dataHash != null, "dataHash is required");

            // TODO
            throw new NotImplementedException();
        }

        public override IObservable<Stream> GetByteStream(string dataHash)
        {
            CheckParameter(dataHash != null, "dataHash is required");

            // TODO
            throw new NotImplementedException();
        }
    }
}