using System;

namespace IO.Proximax.SDK.Exceptions
{
    public class UploadParameterDataNotSupportedException : SystemException
    {
        public UploadParameterDataNotSupportedException(string message) : base(message) { }

        public UploadParameterDataNotSupportedException(string message, Exception innerException): base(message, innerException) { }
    }
}
