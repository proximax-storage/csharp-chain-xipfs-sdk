using System;
using System.Collections.Generic;
using System.IO;
using static Proximax.Storage.SDK.Utils.ParameterValidationUtils;

namespace Proximax.Storage.SDK.Upload
{
    public class StreamParameterData : IByteStreamParameterData
    {
        public Func<Stream> StreamSupplier { get; }

        public StreamParameterData(Func<Stream> streamSupplier, string description, string name, string contentType,
            IDictionary<string, string> metadata)
            : base(description, name, contentType, metadata)
        {
            CheckParameter(streamSupplier != null, "streamSupplier is required");

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