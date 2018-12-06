using System;

namespace Proximax.Storage.SDK.Exceptions
{
    public class DigestDoesNotMatchException : SystemException
    {
        public DigestDoesNotMatchException(string message) : base(message)
        {
        }

        public DigestDoesNotMatchException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}