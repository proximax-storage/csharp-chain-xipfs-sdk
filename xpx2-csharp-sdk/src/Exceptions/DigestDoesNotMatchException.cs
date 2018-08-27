using System;
using System.Collections.Generic;
using System.Text;

namespace IO.Proximax.SDK.Exceptions
{
    class DigestDoesNotMatchException : SystemException
    {
        public DigestDoesNotMatchException(string message) : base(message) { }

        public DigestDoesNotMatchException(string message, Exception innerException): base(message, innerException) { }
    }
}
