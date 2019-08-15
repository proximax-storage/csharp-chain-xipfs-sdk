using System;

namespace ProximaX.Sirius.Storage.SDK.Exceptions
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