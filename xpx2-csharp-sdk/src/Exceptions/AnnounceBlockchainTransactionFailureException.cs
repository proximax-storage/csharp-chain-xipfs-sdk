using System;
using System.Collections.Generic;
using System.Text;

namespace IO.Proximax.SDK.Exceptions
{
    class AnnounceBlockchainTransactionFailureException : SystemException
    {
        public AnnounceBlockchainTransactionFailureException(string message) : base(message) { }

        public AnnounceBlockchainTransactionFailureException(string message, Exception innerException): base(message, innerException) { }
    }
}
