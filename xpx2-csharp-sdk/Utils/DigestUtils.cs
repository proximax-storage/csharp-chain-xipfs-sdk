using System;
using System.IO;
using System.Reactive.Linq;
using IO.Proximax.SDK.Exceptions;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

//TODO
namespace IO.Proximax.SDK.Utils
{
    public class DigestUtils
    {
        public IObservable<string> Digest(Stream byteStream)
        {
            CheckParameter(byteStream != null, "byteStream is required");

            throw new NotImplementedException();
        }

        public IObservable<bool> ValidateDigest(Stream byteStream, string expectedDigest)
        {
            CheckParameter(byteStream != null, "byteStream is required");

            if (expectedDigest != null)
            {
                return Digest(byteStream).Select(actualDigest => {
                    if (!actualDigest.Equals(expectedDigest))
                    {
                        throw new DigestDoesNotMatchException($"Data digests do not match (actual: {actualDigest}, expected {expectedDigest})");
                    }
                    else
                    {
                        return true;
                    }
                });
            }
            return Observable.Return<bool>(true);
        }
    }
}
