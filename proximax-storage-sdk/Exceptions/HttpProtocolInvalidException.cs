using System;

namespace Proximax.Storage.SDK.Exceptions
{
    public class HttpProtocolInvalidException : SystemException
    {
        public HttpProtocolInvalidException(string message) : base(message)
        {
        }

        public HttpProtocolInvalidException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}