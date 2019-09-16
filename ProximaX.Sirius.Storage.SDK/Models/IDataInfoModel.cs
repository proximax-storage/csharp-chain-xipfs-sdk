using System.Collections.Generic;
using Newtonsoft.Json;

namespace ProximaX.Sirius.Storage.SDK.Models
{
    public abstract class IDataInfoModel
    {
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; }

        [JsonProperty("metadata", NullValueHandling = NullValueHandling.Ignore)]
        public IDictionary<string, string> Metadata { get; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; }

        [JsonProperty("contentType", NullValueHandling = NullValueHandling.Ignore)]
        public string ContentType { get; }

        public IDataInfoModel(string description, string name, string contentType, IDictionary<string, string> metadata)
        {
            Description = description;
            Metadata = metadata;
            Name = name;
            ContentType = contentType;
        }
    }
}