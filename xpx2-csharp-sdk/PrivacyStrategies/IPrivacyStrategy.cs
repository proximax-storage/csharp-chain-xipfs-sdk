using System.IO;
using io.nem2.sdk.Model.Transactions.Messages;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

namespace IO.Proximax.SDK.PrivacyStrategies
{
    public abstract class IPrivacyStrategy
    {
        public abstract int GetPrivacyType();
        public abstract Stream EncryptStream(Stream data);
        public abstract Stream DecryptStream(Stream data);
    }
}
