using System;
using System.Collections.Generic;
using System.Text;

namespace IO.Proximax.SDK.Exceptions
{
    class GetTransactionFailureException : SystemException
    {
        public GetTransactionFailureException(string message) : base(message) { }

        public GetTransactionFailureException(string message, Exception innerException): base(message, innerException) { }
    }
}
