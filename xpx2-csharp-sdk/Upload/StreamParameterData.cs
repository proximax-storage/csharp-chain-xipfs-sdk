using System;
using System.Collections.Generic;
using System.IO;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

namespace IO.Proximax.SDK.Upload
{
    public class StreamParameterData : IByteStreamParameterData
    {
        public Func<Stream> StreamSupplier { get; }

        public StreamParameterData(Func<Stream> streamSupplier, string description, string name, string contentType,
            IDictionary<string, string> metadata)
            : base(description, name, contentType, metadata)
        {
            CheckParameter(streamSupplier != streamSupplier, "file is required");

            StreamSupplier = streamSupplier;
        }

        public override Stream GetByteStream()
        {
            return StreamSupplier.Invoke();
        }

        public static StreamParameterData Create(Func<Stream> streamSupplier, string description = null,
            string name = null, string contentType = null, IDictionary<string, string> metadata = null)
        {
            return new StreamParameterData(streamSupplier, description, name, contentType, metadata);
        }
    }
}