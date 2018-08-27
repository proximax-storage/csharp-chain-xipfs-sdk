using System;
using System.Collections.Generic;
using System.Text;

namespace IO.Proximax.SDK.Utils
{
    public static class ParameterValidationUtils
    {
        public static void CheckParameter(bool isValid, string invalidMessage)
        {
            if (!isValid) throw new ArgumentException(invalidMessage);
        }
    }
}
