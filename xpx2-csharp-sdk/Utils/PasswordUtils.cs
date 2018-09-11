using System.Text;
using IO.Proximax.SDK.PrivacyStrategies;
using Org.BouncyCastle.Security;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

namespace IO.Proximax.SDK.Utils
{
    public static class PasswordUtils
    {        
        private const string AlphaNumeric = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private const string SpecialCharacters = "~!@#$%^&*()-_=+[]{};:'\",./<>?";

        public static string generatePassword(int length = SecuredWithPasswordPrivacyStrategy.MinimumPasswordLength,
            bool allowSpecialCharacters = false) {

            CheckParameter(length >= SecuredWithPasswordPrivacyStrategy.MinimumPasswordLength,
                $"Password length should be longer than {SecuredWithPasswordPrivacyStrategy.MinimumPasswordLength}. " +
                $"{length} was provided");

            var random = new SecureRandom();
            var allowedCharacters = allowSpecialCharacters ? AlphaNumeric + SpecialCharacters : AlphaNumeric;

            var stringBuilder = new StringBuilder();
            for (var i = 0; i < length; i++) {
                var index = random.Next(0, allowedCharacters.Length);
                stringBuilder.Append(allowedCharacters[index]);
            }
            return stringBuilder.ToString();
        }

    }
}
