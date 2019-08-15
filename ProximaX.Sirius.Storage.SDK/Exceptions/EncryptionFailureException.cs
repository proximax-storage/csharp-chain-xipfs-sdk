using System;

namespace ProximaX.Sirius.Storage.SDK.Exceptions
{
    public class EncryptionFailureException : SystemException
    {
        public EncryptionFailureException(string message) : base(message)
        {
        }

        public EncryptionFailureException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}