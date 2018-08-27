using System;
using System.Collections.Generic;
using System.Text;

namespace IO.Proximax.SDK.Exceptions
{
    class UploadFailureException : SystemException
    {
        public UploadFailureException(string message) : base(message) { }

        public UploadFailureException(string message, Exception innerException): base(message, innerException) { }
    }
}
