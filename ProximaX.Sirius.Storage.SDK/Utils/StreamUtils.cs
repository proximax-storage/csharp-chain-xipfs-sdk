using System.Diagnostics;
using System.IO;
using System.Text;
using static ProximaX.Sirius.Storage.SDK.Utils.ParameterValidationUtils;

namespace ProximaX.Sirius.Storage.SDK.Utils
{
    public static class StreamUtils
    {
        public static byte[] ReadExactly(this Stream stream, int count)
        {
            var buffer = new byte[count];
            var offset = 0;
            while (offset < count)
            {
                var read = stream.Read(buffer, offset, count - offset);
                if (read == 0)
                    throw new EndOfStreamException();
                offset += read;
            }

            Debug.Assert(offset == count);
            return buffer;
        }

        public static string GetContentAsString(this Stream stream, Encoding encoding = null)
        {
            using (stream)
            {
                using (var reader = new StreamReader(stream, encoding ?? Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public static byte[] GetContentAsByteArray(this Stream stream)
        {
            using (stream)
            {
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }

        public static void SaveToFile(this Stream stream, string file)
        {
            CheckParameter(file != null, "file is required");

            using (stream)
            {
                using (var fileStream = new FileStream(file, FileMode.OpenOrCreate))
                {
                    stream.CopyTo(fileStream);
                }
            }
        }
    }
}