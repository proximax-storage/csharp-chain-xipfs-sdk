using System.Collections.Generic;
using ProximaX.Sirius.Storage.SDK.Models;

namespace ProximaX.Sirius.Storage.SDK.Upload
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