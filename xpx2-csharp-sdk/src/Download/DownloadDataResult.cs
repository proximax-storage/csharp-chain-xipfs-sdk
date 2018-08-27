using System;
using System.Collections.Generic;
using System.Text;

namespace IO.Proximax.SDK.Download
{
    public class DownloadDataResult
    {
        public byte[] Data { get; }

        internal DownloadDataResult(byte[] data)
        {
            Data = data;
        }
    }
}
