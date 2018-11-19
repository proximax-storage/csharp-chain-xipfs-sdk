using System;

namespace IO.Proximax.SDK.Exceptions
{
    public class InvalidPrivateKeyOnDownloadException : SystemException
    {
        public InvalidPrivateKeyOnDownloadException(string message) : base(message) { }

        public InvalidPrivateKeyOnDownloadException(string message, Exception innerException): base(message, innerException) { }
    }
}
