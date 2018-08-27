using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace IO.Proximax.SDK.Models
{
    public abstract class IDataInfoModel
    {
        public string Description { get; }
        public IDictionary<string, string> Metadata { get; }
        public string Name { get; }
        public string ContentType { get; }

        public IDataInfoModel(string description, string name, string contentType, IDictionary<string, string> metadata)
        {
            Description = description;
            Metadata = metadata == null ? ImmutableDictionary<string, string>.Empty : metadata.ToImmutableDictionary();
            Name = name;
            ContentType = contentType;
        }
    }
}
