using System;

namespace IO.Proximax.SDK.Exceptions
{
    public class AnnounceBlockchainTransactionFailureException : SystemException
    {
        public AnnounceBlockchainTransactionFailureException(string message) : base(message) { }

        public AnnounceBlockchainTransactionFailureException(string message, Exception innerException): base(message, innerException) { }
    }
}
