using System;

namespace Proximax.Storage.SDK.Exceptions
{
    public class SearchFailureException : SystemException
    {
        public SearchFailureException(string message) : base(message)
        {
        }

        public SearchFailureException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}