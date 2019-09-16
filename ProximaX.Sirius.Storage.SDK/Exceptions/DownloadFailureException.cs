using System;

namespace ProximaX.Sirius.Storage.SDK.Exceptions
{
    public class DownloadFailureException : SystemException
    {
        public DownloadFailureException(string message) : base(message)
        {
        }

        public DownloadFailureException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}