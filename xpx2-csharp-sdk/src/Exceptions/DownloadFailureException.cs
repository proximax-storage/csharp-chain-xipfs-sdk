using System;
using System.Collections.Generic;
using System.Text;

namespace IO.Proximax.SDK.Exceptions
{
    class DownloadFailureException : SystemException
    {
        public DownloadFailureException(string message) : base(message) { }

        public DownloadFailureException(string message, Exception innerException): base(message, innerException) { }
    }
}
