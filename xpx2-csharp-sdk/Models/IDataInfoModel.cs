using System.Collections.Generic;
using System.Collections.Immutable;
using Newtonsoft.Json;

namespace IO.Proximax.SDK.Models
{
    public abstract class IDataInfoModel
    {
        [JsonProperty("description")]
        public string Description { get; }
        [JsonProperty("metadata")]
        public IDictionary<string, string> Metadata { get; }
        [JsonProperty("name")]
        public string Name { get; }
        [JsonProperty("contentType")]
        public string ContentType { get; }

        public IDataInfoModel(string description, string name, string contentType, IDictionary<string, string> metadata)
        {
            Description = description;
            Metadata = metadata?.ToImmutableDictionary();
            Name = name;
            ContentType = contentType;
        }
    }
}
