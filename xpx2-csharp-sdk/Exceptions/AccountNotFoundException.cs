using System;

namespace IO.Proximax.SDK.Exceptions
{
    public class AccountNotFoundException : SystemException
    {
        public AccountNotFoundException(string message) : base(message) { }

        public AccountNotFoundException(string message, Exception innerException): base(message, innerException) { }
    }
}
