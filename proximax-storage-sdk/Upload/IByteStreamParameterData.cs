using System.Collections.Generic;
using System.IO;
using static Proximax.Storage.SDK.Utils.ParameterValidationUtils;
using static Proximax.Storage.SDK.Models.Constants;

namespace Proximax.Storage.SDK.Upload
{
    public abstract class IByteStreamParameterData : IUploadParameterData
    {
        protected IByteStreamParameterData(string description, string name, string contentType,
            IDictionary<string, string> metadata)
            : base(description, name, contentType, metadata)
        {
            CheckParameter(contentType == null || !ReservedContentTypes.Contains(contentType),
                $"{contentType} cannot be used as it is reserved");
        }

        public abstract Stream GetByteStream();
    }
}