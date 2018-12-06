using System;

namespace Proximax.Storage.SDK.Exceptions
{
    public class GetTransactionFailureException : SystemException
    {
        public GetTransactionFailureException(string message) : base(message)
        {
        }

        public GetTransactionFailureException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}