using io.nem2.sdk.Model.Accounts;
using io.nem2.sdk.Model.Blockchain;
using IO.Proximax.SDK.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

//TODO
namespace IO.Proximax.SDK.Utils
{
    public class DigestUtils
    {

        public IObservable<string> Digest(byte[] data)
        {
            CheckParameter(data != null, "data is required");

            throw new NotImplementedException();
        }

        public IObservable<bool> ValidateDigest(byte[] data, string expectedDigest)
        {
            CheckParameter(data != null, "data is required");

            if (expectedDigest != null)
            {
                return Digest(data).Select(actualDigest => {
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
