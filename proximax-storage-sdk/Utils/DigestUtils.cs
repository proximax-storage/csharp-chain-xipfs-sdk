using System;
using System.IO;
using Proximax.Storage.SDK.Exceptions;
using static Proximax.Storage.SDK.Utils.ParameterValidationUtils;

//TODO
namespace Proximax.Storage.SDK.Utils
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
                throw new DigestDoesNotMatchException(
                    $"Data digests do not match (actual: {actualDigest}, expected {expectedDigest})");
            }
            else
            {
                return true;
            }
        }
    }
}