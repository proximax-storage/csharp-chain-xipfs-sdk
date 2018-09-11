using System;
using System.Collections.Generic;
using System.IO;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

namespace IO.Proximax.SDK.Upload
{
    public class FileParameterData : IByteStreamParameterData
    {
        public string Filename { get; }
        
        public FileParameterData(string file, string description, string name, string contentType, IDictionary<string, string> metadata) 
            : base(description, GetDefaultName(file, name), contentType, metadata)
        {
            CheckParameter(file != null, "file is required");
            CheckParameter(() => !File.GetAttributes(file).HasFlag(FileAttributes.Directory), "file is not file");

            Filename = file;
        }

        public override Stream GetByteStream()
        {
            return File.Open(Filename, FileMode.Open, FileAccess.Read);
        }
        
        public static FileParameterData Create(string filename, string description = null, string name = null, string contentType = null, IDictionary<string, string> metadata = null) {
            return new FileParameterData(filename, description, name, contentType, metadata);
        }
        
        private static string GetDefaultName(string filename, String name) {
            return name == null && filename != null ? Path.GetFileName(filename) : name;
        }
    }

}
