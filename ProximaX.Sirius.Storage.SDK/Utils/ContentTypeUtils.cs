using System.IO;
using static ProximaX.Sirius.Storage.SDK.Utils.ParameterValidationUtils;

//TODO
namespace ProximaX.Sirius.Storage.SDK.Utils
{
    public static class ContentTypeUtils
    {
        public static string DetectContentType(this Stream stream)
        {
            CheckParameter(stream != null, "stream is required");

//            using (stream)
//            {
//                var contentType = stream.GetFileType();
//                return contentType?.Mime;
//            }
            return null;
        }
    }
}