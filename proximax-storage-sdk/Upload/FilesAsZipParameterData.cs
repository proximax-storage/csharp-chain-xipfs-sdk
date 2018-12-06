using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using static Proximax.Storage.SDK.Utils.ParameterValidationUtils;

namespace Proximax.Storage.SDK.Upload
{
    public class FilesAsZipParameterData : IByteStreamParameterData
    {
        private byte[] ZipData { get; }

        public IEnumerable<string> Files { get; }

        public FilesAsZipParameterData(IEnumerable<string> files, string description, string name,
            IDictionary<string, string> metadata)
            : base(description, name, "application/zip", metadata)
        {
            CheckParameter(files?.Any() == true, "files cannot be null or empty");
            CheckParameter(() => files.All(file => !File.GetAttributes(file).HasFlag(FileAttributes.Directory)),
                "not all files are file");

            Files = files;
            ZipData = ZipFiles(files);
        }

        public override Stream GetByteStream()
        {
            return new MemoryStream(ZipData);
        }

        public static FilesAsZipParameterData Create(IEnumerable<string> files, string description = null,
            string name = null, IDictionary<string, string> metadata = null)
        {
            return new FilesAsZipParameterData(files, description, name, metadata);
        }

        private static byte[] ZipFiles(IEnumerable<string> files)
        {
            using (var zipToCreate = new MemoryStream())
            {
                using (var archive = new ZipArchive(zipToCreate, ZipArchiveMode.Create, true))
                {
                    foreach (var file in files)
                    {
                        using (var csvStream = new FileStream(file, FileMode.Open, FileAccess.Read))
                        {
                            var fileEntry = archive.CreateEntry(Path.GetFileName(file));
                            using (var entryStream = fileEntry.Open())
                            {
                                csvStream.CopyTo(entryStream);
                            }
                        }
                    }
                }

                return zipToCreate.ToArray();
            }
        }
    }
}