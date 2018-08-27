using System;
using System.Collections.Generic;
using System.Text;

namespace IO.Proximax.SDK.Exceptions
{
    class UploadParameterBuildFailureException : SystemException
    {
        public UploadParameterBuildFailureException(string message) : base(message) { }

        public UploadParameterBuildFailureException(string message, Exception innerException): base(message, innerException) { }
    }
}
