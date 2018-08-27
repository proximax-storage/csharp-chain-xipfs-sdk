using System;
using System.Collections.Generic;
using System.Text;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;
using static IO.Proximax.SDK.Models.Constants;

namespace IO.Proximax.SDK.Upload
{
    public class ByteArrayParameterData : IUploadParameterData
    {
        public byte[] Data { get; }

        public ByteArrayParameterData(byte[] data, string description, string name, string contentType, IDictionary<string, string> metadata)
            : base(description, name, contentType, metadata)
        {
            Data = data;
        }

        public static ByteArrayParameterDataBuilder Create(byte[] data)
        {
            CheckParameter(data != null, "data is required");

            return new ByteArrayParameterDataBuilder(data);
        }
    }

    public class ByteArrayParameterDataBuilder : IParameterDataBuilder<ByteArrayParameterDataBuilder>
    {
        private byte[] Data { get; set; }
        private string ContentType { get; set; }

        public ByteArrayParameterDataBuilder(byte[] data)
        {
            Data = data;
        }

        public ByteArrayParameterDataBuilder WithContentType(String contentType)
        {
            CheckParameter(contentType == null ||
                !RESERVED_CONTENT_TYPES.Contains(contentType), $"{contentType} cannot be used as it is reserved");

            ContentType = contentType;
            return this;
        }

        public ByteArrayParameterData Build()
        {
            return new ByteArrayParameterData(Data, Description, Name, ContentType, Metadata);
        }
    }

}
