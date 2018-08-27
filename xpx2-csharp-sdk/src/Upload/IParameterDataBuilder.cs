using System;
using System.Collections.Generic;
using System.Text;

namespace IO.Proximax.SDK.Upload
{
    public class IParameterDataBuilder<T> where T: IParameterDataBuilder<T>
    {
        internal string Description { get; set; }
        internal IDictionary<string, string> Metadata { get; set; }
        internal string Name { get; set; }

        internal IParameterDataBuilder() { }

        public T WithDescription(string description)
        {
            Description = description;
            return (T)this;
        }

        public T WithMetadata(IDictionary<string, string> metadata)
        {
            Metadata = metadata;
            return (T)this;
        }

        public T WithName(string name)
        {
            Name = name;
            return (T)this;
        }
    }
}
