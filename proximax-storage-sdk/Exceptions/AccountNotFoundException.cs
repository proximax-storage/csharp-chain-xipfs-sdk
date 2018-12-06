using System;

namespace Proximax.Storage.SDK.Exceptions
{
    public class AccountNotFoundException : SystemException
    {
        public AccountNotFoundException(string message) : base(message)
        {
        }

        public AccountNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}