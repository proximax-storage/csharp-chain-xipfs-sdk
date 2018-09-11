using System;

namespace IO.Proximax.SDK.Exceptions
{
    public class DownloadInitFailureException : SystemException
    {
        public DownloadInitFailureException(string message) : base(message) { }

        public DownloadInitFailureException(string message, Exception innerException): base(message, innerException) { }
    }
}
