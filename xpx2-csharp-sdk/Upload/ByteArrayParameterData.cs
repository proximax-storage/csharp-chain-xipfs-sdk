using System.Collections.Generic;
using System.IO;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

namespace IO.Proximax.SDK.Upload
{
    public class ByteArrayParameterData : IByteStreamParameterData
    {
        public byte[] Data { get; }

        public ByteArrayParameterData(byte[] data, string description, string name, string contentType, IDictionary<string, string> metadata)
            : base(description, name, contentType, metadata)
        {            
            CheckParameter(data != null, "data is required");

            Data = data;
        }

        public override Stream GetByteStream()
        {
            return new MemoryStream(Data);
        }

        public static ByteArrayParameterData Create(byte[] data, string description = null, string name = null, string contentType = null, IDictionary<string, string> metadata = null) {
            return new ByteArrayParameterData(data, description, name, contentType, metadata);
        }
    }
}
