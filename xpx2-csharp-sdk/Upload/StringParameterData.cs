using System.Collections.Generic;
using System.IO;
using System.Text;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

namespace IO.Proximax.SDK.Upload
{
    public class StringParameterData : IByteStreamParameterData
    {
        private byte[] StringData { get; }

        public string @String { get; }

        public StringParameterData(string @string, Encoding encoding, string description, string name, string contentType, IDictionary<string, string> metadata) 
            : base(description, name, contentType, metadata)
        {           
            CheckParameter(@string != null, "@string is required");

            @String = @string;
            StringData = GetStringByteArray(@string, encoding);
        }
        
        public override Stream GetByteStream() {
            return new MemoryStream(StringData);
        }
        
        private byte[] GetStringByteArray(string @string, Encoding encoding) {
            return encoding == null ? Encoding.UTF8.GetBytes(@string) : encoding.GetBytes(@string);
        }

        public static StringParameterData Create(string @string, Encoding encoding = null, string description = null, 
            string name = null, string contentType = null, IDictionary<string, string> metadata = null) {
            return new StringParameterData(@string, encoding, description, name, contentType, metadata);
        }
    }

}
