using System;
using System.Collections.Generic;
using System.Text;

namespace IO.Proximax.SDK.Models
{
    public class ProximaxDataModel: IDataInfoModel
    {
        public string Digest { get; }
        public string DataHash { get; }
        public long Timestamp { get; }

        public ProximaxDataModel(string digest, string dataHash, string description, IDictionary<string, string> metadata,
            long timestamp, string name, string contentType): base(description, name, contentType, metadata)
        {
            Digest = digest;
            DataHash = dataHash;
            Timestamp = timestamp;
        }
    }
}
