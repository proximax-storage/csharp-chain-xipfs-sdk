using System;

namespace ProximaX.Sirius.Storage.SDK.Exceptions
{
    public class DownloadInitFailureException : SystemException
    {
        public DownloadInitFailureException(string message) : base(message)
        {
        }

        public DownloadInitFailureException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}