using System.Collections.Generic;
using Newtonsoft.Json;

namespace IO.Proximax.SDK.Models
{
    public class ProximaxDataModel: IDataInfoModel
    {
        [JsonProperty("digest", NullValueHandling=NullValueHandling.Ignore)]
        public string Digest { get; }
        [JsonProperty("dataHash", NullValueHandling=NullValueHandling.Ignore)]
        public string DataHash { get; }
        [JsonProperty("timestamp", NullValueHandling=NullValueHandling.Ignore)]
        public long Timestamp { get; }

        public ProximaxDataModel(string digest, string dataHash, string description, IDictionary<string, string> metadata,
            long timestamp, string name, string contentType): base(description, name, contentType, metadata)
        {
            Digest = digest;
            DataHash = dataHash;
            Timestamp = timestamp;
        }
        
        public static ProximaxDataModel Create(IDataInfoModel parameterData, string dataHash, string digest, string contentType, long timestamp) {
            return new ProximaxDataModel(digest, dataHash, parameterData.Description, parameterData.Metadata,
                timestamp, parameterData.Name, contentType);
        }
    }
}
