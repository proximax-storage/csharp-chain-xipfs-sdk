using System;
using System.Collections.Generic;
using System.Text;
using IO.Proximax.SDK.Models;

namespace IO.Proximax.SDK.Upload
{
    public class IUploadParameterData: IDataInfoModel
    {
        public IUploadParameterData(string description, string name, string contentType, IDictionary<string, string> metadata)
            : base(description, name, contentType, metadata) { }
    }
}
