using System;

namespace Proximax.Storage.SDK.Exceptions
{
    public class PublicKeyNotFoundException : SystemException
    {
        public PublicKeyNotFoundException(string message) : base(message)
        {
        }

        public PublicKeyNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}