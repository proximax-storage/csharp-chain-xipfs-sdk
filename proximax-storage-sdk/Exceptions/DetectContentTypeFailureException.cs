using System;

namespace Proximax.Storage.SDK.Exceptions
{
    public class DetectContentTypeFailureException : SystemException
    {
        public DetectContentTypeFailureException(string message) : base(message)
        {
        }

        public DetectContentTypeFailureException(string message, Exception innerException) : base(message,
            innerException)
        {
        }
    }
}