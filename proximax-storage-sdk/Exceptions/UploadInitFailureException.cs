using System;

namespace Proximax.Storage.SDK.Exceptions
{
    public class UploadInitFailureException : SystemException
    {
        public UploadInitFailureException(string message) : base(message)
        {
        }

        public UploadInitFailureException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}