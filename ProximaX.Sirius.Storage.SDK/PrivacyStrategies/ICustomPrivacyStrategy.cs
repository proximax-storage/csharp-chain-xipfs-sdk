using ProximaX.Sirius.Storage.SDK.Models;

namespace ProximaX.Sirius.Storage.SDK.PrivacyStrategies
{
    public abstract class ICustomPrivacyStrategy : IPrivacyStrategy
    {
        public sealed override int GetPrivacyType() => (int) PrivacyType.Custom;
    }
}