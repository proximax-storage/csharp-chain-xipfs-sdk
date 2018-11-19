using System;

namespace IO.Proximax.SDK.Exceptions
{
    public class MissingPrivateKeyOnDownloadException : SystemException
    {
        public MissingPrivateKeyOnDownloadException(string message) : base(message) { }

        public MissingPrivateKeyOnDownloadException(string message, Exception innerException): base(message, innerException) { }
    }
}
