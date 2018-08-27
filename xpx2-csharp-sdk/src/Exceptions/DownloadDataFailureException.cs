using System;
using System.Collections.Generic;
using System.Text;

namespace IO.Proximax.SDK.Exceptions
{
    class DownloadDataFailureException : SystemException
    {
        public DownloadDataFailureException(string message) : base(message) { }

        public DownloadDataFailureException(string message, Exception innerException): base(message, innerException) { }
    }
}
