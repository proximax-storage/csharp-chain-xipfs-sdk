using System;
using System.Collections.Generic;
using System.Text;

namespace IO.Proximax.SDK.Exceptions
{
    class TransactionNotAllowedException : SystemException
    {
        public TransactionNotAllowedException(string message) : base(message) { }

        public TransactionNotAllowedException(string message, Exception innerException): base(message, innerException) { }
    }
}
