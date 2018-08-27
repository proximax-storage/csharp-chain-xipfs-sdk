using System;
using System.Collections.Generic;
using System.Text;

namespace IO.Proximax.SDK.Exceptions
{
    class DecodeTransactionMessageFailureException : SystemException
    {
        public DecodeTransactionMessageFailureException(string message) : base(message) { }

        public DecodeTransactionMessageFailureException(string message, Exception innerException): base(message, innerException) { }
    }
}
