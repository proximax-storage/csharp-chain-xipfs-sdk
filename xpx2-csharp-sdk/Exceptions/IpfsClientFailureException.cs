using System;

namespace IO.Proximax.SDK.Exceptions
{
    public class IpfsClientFailureException : SystemException
    {
        public IpfsClientFailureException(string message) : base(message) { }

        public IpfsClientFailureException(string message, Exception innerException): base(message, innerException) { }
    }
}
