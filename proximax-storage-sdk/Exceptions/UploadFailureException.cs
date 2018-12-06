using System;

namespace Proximax.Storage.SDK.Exceptions
{
    public class UploadFailureException : SystemException
    {
        public UploadFailureException(string message) : base(message)
        {
        }

        public UploadFailureException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}