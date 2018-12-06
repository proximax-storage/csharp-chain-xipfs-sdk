using System;

namespace Proximax.Storage.SDK.Exceptions
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