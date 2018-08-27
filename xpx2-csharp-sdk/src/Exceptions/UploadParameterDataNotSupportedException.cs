using System;
using System.Collections.Generic;
using System.Text;

namespace IO.Proximax.SDK.Exceptions
{
    class UploadParameterDataNotSupportedException : SystemException
    {
        public UploadParameterDataNotSupportedException(string message) : base(message) { }

        public UploadParameterDataNotSupportedException(string message, Exception innerException): base(message, innerException) { }
    }
}
