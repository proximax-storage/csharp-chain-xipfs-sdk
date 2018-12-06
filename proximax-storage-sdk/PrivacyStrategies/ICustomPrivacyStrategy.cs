using Proximax.Storage.SDK.Models;

namespace Proximax.Storage.SDK.PrivacyStrategies
{
    public abstract class ICustomPrivacyStrategy : IPrivacyStrategy
    {
        public sealed override int GetPrivacyType() => (int) PrivacyType.Custom;
    }
}