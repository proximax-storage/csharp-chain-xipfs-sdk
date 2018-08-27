using System;
using System.Collections.Generic;
using System.Text;
using IO.Proximax.SDK.Models;

namespace IO.Proximax.SDK.Download
{
    public class DownloadResultData: IDataInfoModel
    {
        public byte[] Data { get; }

        internal DownloadResultData(byte[] data, string description, string name, string contentType, 
            IDictionary<string, string> metadata): base(description, name, contentType, metadata)
        {
            Data = data;
        }
    }
}
