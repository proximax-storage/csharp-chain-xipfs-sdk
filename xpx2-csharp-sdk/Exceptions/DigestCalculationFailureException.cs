using System;

namespace IO.Proximax.SDK.Exceptions
{
    public class DigestCalculationFailureException : SystemException
    {
        public DigestCalculationFailureException(string message) : base(message) { }

        public DigestCalculationFailureException(string message, Exception innerException): base(message, innerException) { }
    }
}
