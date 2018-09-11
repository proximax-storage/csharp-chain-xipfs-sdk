using System;

namespace IO.Proximax.SDK.Exceptions
{
    public class DownloadForMessageTypeNotSupportedException : SystemException
    {
        public DownloadForMessageTypeNotSupportedException(string message) : base(message) { }

        public DownloadForMessageTypeNotSupportedException(string message, Exception innerException): base(message, innerException) { }
    }
}
