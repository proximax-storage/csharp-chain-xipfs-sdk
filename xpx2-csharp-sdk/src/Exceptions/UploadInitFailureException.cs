using System;
using System.Collections.Generic;
using System.Text;

namespace IO.Proximax.SDK.Exceptions
{
    class UploadInitFailureException : SystemException
    {
        public UploadInitFailureException(string message) : base(message) { }

        public UploadInitFailureException(string message, Exception innerException): base(message, innerException) { }
    }
}
