using System;
using System.Collections.Generic;

using System.IO;
using System.Linq;
using System.Text;
using ProximaX.Sirius.Storage.SDK.Models;
using ProximaX.Sirius.Storage.SDK.Utils;

namespace ProximaX.Sirius.Storage.SDK.Download
{
    public class DownloadResultData : IDataInfoModel
    {
        private Func<Stream> ByteStreamSupplier { get; }

        public string Digest { get; }
        public string DataHash { get; }
        public long Timestamp { get; }



        public DownloadResultData(Func<Stream> byteStreamSupplier, string digest, string dataHash, long timestamp,
            string description, string name, string contentType, IDictionary<string, string> metadata)
            : base(description, name, contentType, metadata)

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

        public string GetContentAsString(Encoding encoding = null)
        {
            return GetByteStream().GetContentAsString(encoding);
        }

        public byte[] GetContentAsByteArray()
        {
            return GetByteStream().GetContentAsByteArray();
        }

        public void SaveToFile(string file)
        {
            GetByteStream().SaveToFile(file);
        }
    }
}