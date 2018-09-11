using System;

namespace IO.Proximax.SDK.Exceptions
{
    public class DownloadFailureException : SystemException
    {
        public DownloadFailureException(string message) : base(message) { }

        public DownloadFailureException(string message, Exception innerException): base(message, innerException) { }
    }
}
