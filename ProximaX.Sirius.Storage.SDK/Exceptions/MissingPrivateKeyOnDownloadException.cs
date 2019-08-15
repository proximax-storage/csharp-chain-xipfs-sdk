using System;

namespace ProximaX.Sirius.Storage.SDK.Exceptions
{
    public class MissingPrivateKeyOnDownloadException : SystemException
    {
        public MissingPrivateKeyOnDownloadException(string message) : base(message)
        {
        }

        public MissingPrivateKeyOnDownloadException(string message, Exception innerException) : base(message,
            innerException)
        {
        }
    }
}