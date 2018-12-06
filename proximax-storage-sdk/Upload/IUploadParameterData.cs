using System.Collections.Generic;
using Proximax.Storage.SDK.Models;

namespace Proximax.Storage.SDK.Upload
{
    public abstract class IUploadParameterData : IDataInfoModel
    {
        public IUploadParameterData(string description, string name, string contentType,
            IDictionary<string, string> metadata)
            : base(description, name, contentType, metadata)
        {
        }
    }
}