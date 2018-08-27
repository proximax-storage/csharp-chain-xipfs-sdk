using System;
using System.Collections.Generic;
using System.Text;

namespace IO.Proximax.SDK.Models
{
    public static class PrivacyType
    {
        public enum Types: int
        {
            PLAIN = 1001,
            NEMKEYS = 1002,
            SHAMIR = 1003,
            PASSWORD = 1004,
            CUSTOM = 2001
        }
    }
}
