using System;

namespace Proximax.Storage.SDK.Exceptions
{
    public class UploadParameterDataNotSupportedException : SystemException
    {
        public UploadParameterDataNotSupportedException(string message) : base(message)
        {
        }

        public UploadParameterDataNotSupportedException(string message, Exception innerException) : base(message,
            innerException)
        {
        }
    }
}