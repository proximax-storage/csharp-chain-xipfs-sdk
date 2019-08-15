using System;

namespace ProximaX.Sirius.Storage.SDK.Exceptions
{
    public class DecryptionFailureException : SystemException
    {
        public DecryptionFailureException(string message) : base(message)
        {
        }

        public DecryptionFailureException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}