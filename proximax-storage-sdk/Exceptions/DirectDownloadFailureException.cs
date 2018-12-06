using System;

namespace Proximax.Storage.SDK.Exceptions
{
    public class DirectDownloadFailureException : SystemException
    {
        public DirectDownloadFailureException(string message) : base(message)
        {
        }

        public DirectDownloadFailureException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}