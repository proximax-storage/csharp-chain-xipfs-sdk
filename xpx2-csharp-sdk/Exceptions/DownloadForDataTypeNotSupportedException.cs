using System;

namespace IO.Proximax.SDK.Exceptions
{
    public class DownloadForDataTypeNotSupportedException : SystemException
    {
        public DownloadForDataTypeNotSupportedException(string message) : base(message) { }

        public DownloadForDataTypeNotSupportedException(string message, Exception innerException): base(message, innerException) { }
    }
}
