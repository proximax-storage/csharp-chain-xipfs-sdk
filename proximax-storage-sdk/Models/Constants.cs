using System.Collections.Generic;

namespace Proximax.Storage.SDK.Models
{
    public static class Constants
    {
        public const string SchemaVersion = "1.0";
        public const string PathUploadContentType = "ipfs/directory";
        public static readonly IList<string> ReservedContentTypes = new List<string> {PathUploadContentType};
    }
}