using System.IO;
using MimeDetective.Extensions;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

//TODO
namespace IO.Proximax.SDK.Utils
{
    public static class ContentTypeUtils
    {

        public static string DetectContentType(this Stream stream)
        {
            CheckParameter(stream != null, "stream is required");

            using (stream)
            {
                var contentType = stream.GetFileType();
                return contentType?.Mime;
            }
        }
    }
}
