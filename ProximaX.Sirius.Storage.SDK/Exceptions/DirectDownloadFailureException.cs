using System;

namespace ProximaX.Sirius.Storage.SDK.Exceptions
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