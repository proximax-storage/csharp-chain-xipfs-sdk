using System.Collections.Generic;
using System.IO;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;
using static IO.Proximax.SDK.Models.Constants;

namespace IO.Proximax.SDK.Upload
{
    public abstract class IByteStreamParameterData : IUploadParameterData
    {
        protected IByteStreamParameterData(string description, string name, string contentType, IDictionary<string, string> metadata) 
            : base(description, name, contentType, metadata)
        {
            CheckParameter(contentType == null || !ReservedContentTypes.Contains(contentType),
                $"{contentType} cannot be used as it is reserved");
        }

        public abstract Stream GetByteStream();
    }

}
