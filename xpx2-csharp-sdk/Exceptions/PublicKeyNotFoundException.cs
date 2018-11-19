using System;

namespace IO.Proximax.SDK.Exceptions
{
    public class PublicKeyNotFoundException : SystemException
    {
        public PublicKeyNotFoundException(string message) : base(message) { }

        public PublicKeyNotFoundException(string message, Exception innerException): base(message, innerException) { }
    }
}
