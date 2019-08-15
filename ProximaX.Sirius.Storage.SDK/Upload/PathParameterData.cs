using System.Collections.Generic;
using System.IO;
using static ProximaX.Sirius.Storage.SDK.Utils.ParameterValidationUtils;
using static ProximaX.Sirius.Storage.SDK.Models.Constants;

namespace ProximaX.Sirius.Storage.SDK.Upload
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

        public static PathParameterData Create(string path, string description = null, string name = null,
            IDictionary<string, string> metadata = null)
        {
            return new PathParameterData(path, description, name, metadata);
        }
    }
}