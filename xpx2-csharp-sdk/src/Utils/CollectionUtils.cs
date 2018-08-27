using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace IO.Proximax.SDK.Utils
{
    public static class CollectionUtils
    {
        public static bool IsEmpty(ICollection collection)
        {
            return collection == null || collection.Count == 0;
        }

        public static bool IsNotEmpty(ICollection collection)
        {
            return collection != null && collection.Count != 0;
        }

    }
}
