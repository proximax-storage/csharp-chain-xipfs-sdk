using System;
using System.Collections.Generic;
using System.Text;

namespace IO.Proximax.SDK.Exceptions
{
    class DownloadInitFailureException : SystemException
    {
        public DownloadInitFailureException(string message) : base(message) { }

        public DownloadInitFailureException(string message, Exception innerException): base(message, innerException) { }
    }
}
