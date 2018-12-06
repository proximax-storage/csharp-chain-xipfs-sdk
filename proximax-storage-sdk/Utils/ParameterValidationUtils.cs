using System;

namespace Proximax.Storage.SDK.Utils
{
    public static class ParameterValidationUtils
    {
        public static void CheckParameter(bool isValid, string invalidMessage)
        {
            if (!isValid) throw new ArgumentException(invalidMessage);
        }

        public static void CheckParameter(Func<bool> isValidFunction, string invalidMessage)
        {
            try
            {
                if (!isValidFunction.Invoke())
                {
                    throw new ArgumentException(invalidMessage);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(invalidMessage, ex);
            }
        }
    }
}