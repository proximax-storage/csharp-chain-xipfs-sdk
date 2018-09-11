namespace IO.Proximax.SDK.PrivacyStrategies
{
    public abstract class ICustomPrivacyStrategy: IPrivacyStrategy
    {
        public sealed override int GetPrivacyType() => (int)Models.PrivacyType.Custom;
    }
}
