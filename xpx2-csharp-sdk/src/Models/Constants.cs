using System;
using System.Collections.Generic;
using System.Text;

namespace IO.Proximax.SDK.Models
{
    public static class Constants
    {
        public static readonly string SCHEMA_VERSION = "1.0";
        public static readonly string PATH_UPLOAD_CONTENT_TYPE = "ipfs/directory";
        public static readonly IList<string> RESERVED_CONTENT_TYPES = new List<string> { PATH_UPLOAD_CONTENT_TYPE };
    }
}
