using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using IO.Proximax.SDK.Models;

namespace IO.Proximax.SDK.Download
{
    public class DownloadResultData : IDataInfoModel
    {
        private Func<Stream> ByteStreamSupplier { get; }

        public string Digest { get; }
        public string DataHash { get; }
        public long Timestamp { get; }

        public DownloadResultData(Func<Stream> byteStreamSupplier, string digest, string dataHash, long timestamp,
            string description, string name, string contentType, IDictionary<string, string> metadata)
            : base(description, name, contentType,
                metadata == null ? ImmutableDictionary<string, string>.Empty : metadata.ToImmutableDictionary())
        {
            ByteStreamSupplier = byteStreamSupplier;
            Digest = digest;
            DataHash = dataHash;
            Timestamp = timestamp;
        }

        public Stream GetByteStream()
        {
            return ByteStreamSupplier.Invoke();
        }

        public string GetContentAsString()
        {
            // TODO
            throw new NotImplementedException();
        }

        public string GetContentAsByteArray()
        {
            // TODO
            throw new NotImplementedException();
        }

        public void SaveToFile(string file)
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}