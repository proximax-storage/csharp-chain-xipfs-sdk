using System.Collections.Generic;
using System.Collections.Immutable;
using Newtonsoft.Json;

namespace IO.Proximax.SDK.Models
{
    public abstract class IDataInfoModel
    {
        [JsonProperty("description", NullValueHandling=NullValueHandling.Ignore)]
        public string Description { get; }
        [JsonProperty("metadata", NullValueHandling=NullValueHandling.Ignore)]
        public IDictionary<string, string> Metadata { get; }
        [JsonProperty("name", NullValueHandling=NullValueHandling.Ignore)]
        public string Name { get; }
        [JsonProperty("contentType", NullValueHandling=NullValueHandling.Ignore)]
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
