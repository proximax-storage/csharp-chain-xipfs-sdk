using System;
using System.Collections.Generic;
using System.Text;
using io.nem2.sdk.Model.Accounts;
using IO.Proximax.SDK.Models;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

// TODO
namespace IO.Proximax.SDK.PrivacyStrategies
{
    public sealed class SecuredWithPasswordPrivacyStrategy : IPlainMessagePrivacyStrategy
    {
        SecuredWithPasswordPrivacyStrategy(string password, string searchTag): base(searchTag) {
            CheckParameter(password != null, "private key is required");
        }

        public override int PrivacyType() => (int) Models.PrivacyType.Types.PASSWORD;

        public override byte[] DecryptData(byte[] data)
        {
            throw new NotImplementedException();
        }

        public override byte[] EncryptData(byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}
