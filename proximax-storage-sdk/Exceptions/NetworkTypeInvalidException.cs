using System;

namespace Proximax.Storage.SDK.Exceptions
{
    public class NetworkTypeInvalidException : SystemException
    {
        public NetworkTypeInvalidException(string message) : base(message)
        {
        }

        public NetworkTypeInvalidException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}