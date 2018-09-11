using System;

namespace IO.Proximax.SDK.Exceptions
{
    public class UploadFailureException : SystemException
    {
        public UploadFailureException(string message) : base(message) { }

        public UploadFailureException(string message, Exception innerException): base(message, innerException) { }
    }
}
