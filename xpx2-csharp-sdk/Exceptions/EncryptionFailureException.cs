using System;

namespace IO.Proximax.SDK.Exceptions
{
    public class EncryptionFailureException : SystemException
    {
        public EncryptionFailureException(string message) : base(message) { }

        public EncryptionFailureException(string message, Exception innerException): base(message, innerException) { }
    }
}
