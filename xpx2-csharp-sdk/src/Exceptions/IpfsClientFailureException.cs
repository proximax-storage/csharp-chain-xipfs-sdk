using System;
using System.Collections.Generic;
using System.Text;

namespace IO.Proximax.SDK.Exceptions
{
    class IpfsClientFailureException : SystemException
    {
        public IpfsClientFailureException(string message) : base(message) { }

        public IpfsClientFailureException(string message, Exception innerException): base(message, innerException) { }
    }
}
