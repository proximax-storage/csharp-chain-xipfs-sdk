using System;

namespace ProximaX.Sirius.Storage.SDK.Exceptions
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