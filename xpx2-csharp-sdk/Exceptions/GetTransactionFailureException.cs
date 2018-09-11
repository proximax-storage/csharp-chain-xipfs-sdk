using System;

namespace IO.Proximax.SDK.Exceptions
{
    public class GetTransactionFailureException : SystemException
    {
        public GetTransactionFailureException(string message) : base(message) { }

        public GetTransactionFailureException(string message, Exception innerException): base(message, innerException) { }
    }
}
