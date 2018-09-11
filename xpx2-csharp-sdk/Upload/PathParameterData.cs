using System.Collections.Generic;
using System.IO;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;
using static IO.Proximax.SDK.Models.Constants;

namespace IO.Proximax.SDK.Upload
{
    public class PathParameterData : IUploadParameterData
    {        
        public string Path { get; }

        public PathParameterData(string path, string description, string name, IDictionary<string, string> metadata) 
            : base(description, name, PathUploadContentType, metadata)
        {
            CheckParameter(path != null, "path is required");
            CheckParameter(File.GetAttributes(path).HasFlag(FileAttributes.Directory), "path is not a directory ");

            Path = path;
        }
        
        public static PathParameterData Create(string path, string description = null, string name = null, IDictionary<string, string> metadata = null) {
            return new PathParameterData(path, description, name, metadata);
        }
        
    }

}
