using System;
using System.IO;
using IO.Proximax.SDK.Exceptions;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

//TODO
namespace IO.Proximax.SDK.Utils
{
    public static class DigestUtils
    {
        public static string Digest(this Stream stream)
        {
            CheckParameter(stream != null, "stream is required");

            using (stream)
            {
                throw new NotImplementedException();
            }
        }

        public static bool ValidateDigest(this Stream stream, string expectedDigest)
        {
            CheckParameter(stream != null, "stream is required");

            if (expectedDigest == null) return true;
            
            var actualDigest = Digest(stream);
            if (!actualDigest.Equals(expectedDigest))
            {
                throw new DigestDoesNotMatchException($"Data digests do not match (actual: {actualDigest}, expected {expectedDigest})");
            }
            else
            {
                return true;
            }
        }
    }
}
