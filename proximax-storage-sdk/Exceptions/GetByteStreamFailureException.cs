using System;

namespace Proximax.Storage.SDK.Exceptions
{
    public class GetByteStreamFailureException : SystemException
    {
        public GetByteStreamFailureException(string message) : base(message)
        {
        }

        public GetByteStreamFailureException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}