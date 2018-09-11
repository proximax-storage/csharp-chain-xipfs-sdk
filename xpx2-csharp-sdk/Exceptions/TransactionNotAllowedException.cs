using System;

namespace IO.Proximax.SDK.Exceptions
{
    public class TransactionNotAllowedException : SystemException
    {
        public TransactionNotAllowedException(string message) : base(message) { }

        public TransactionNotAllowedException(string message, Exception innerException): base(message, innerException) { }
    }
}
