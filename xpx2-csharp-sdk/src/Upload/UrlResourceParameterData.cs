using System;
using System.Collections.Generic;
using System.Text;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;
using static IO.Proximax.SDK.Models.Constants;

// TODO
namespace IO.Proximax.SDK.Upload
{
    public class UrlResourceParameterData : ByteArrayParameterData
    {
        public UrlResourceParameterData(byte[] data, string description, string name, string contentType, IDictionary<string, string> metadata)
            : base(data, description, name, contentType, metadata)
        {
        }
    }

}
