using System;
using System.Collections.Generic;
using System.Text;

namespace IO.Proximax.SDK.Exceptions
{
    class EncryptionFailureException : SystemException
    {
        public EncryptionFailureException(string message) : base(message) { }

        public EncryptionFailureException(string message, Exception innerException): base(message, innerException) { }
    }
}
