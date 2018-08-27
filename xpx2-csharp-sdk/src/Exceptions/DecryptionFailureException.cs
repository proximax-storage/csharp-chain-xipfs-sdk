using System;
using System.Collections.Generic;
using System.Text;

namespace IO.Proximax.SDK.Exceptions
{
    class DecryptionFailureException : SystemException
    {
        public DecryptionFailureException(string message) : base(message) { }

        public DecryptionFailureException(string message, Exception innerException): base(message, innerException) { }
    }
}
