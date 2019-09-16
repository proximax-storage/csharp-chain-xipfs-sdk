using System;

namespace ProximaX.Sirius.Storage.SDK.Exceptions
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