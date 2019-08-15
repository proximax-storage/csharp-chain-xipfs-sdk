using System.Collections.Generic;
using System.IO;
using static ProximaX.Sirius.Storage.SDK.Utils.ParameterValidationUtils;
using static ProximaX.Sirius.Storage.SDK.Models.Constants;

namespace ProximaX.Sirius.Storage.SDK.Upload
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