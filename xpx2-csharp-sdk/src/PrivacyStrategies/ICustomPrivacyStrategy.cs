using System;
using System.Collections.Generic;
using System.Text;
using IO.Proximax.SDK.Models;

namespace IO.Proximax.SDK.PrivacyStrategies
{
    public abstract class ICustomPrivacyStrategy: IPlainMessagePrivacyStrategy
    {
        public ICustomPrivacyStrategy(string searchTag): base(searchTag) { }

        public sealed override int PrivacyType() => (int)Models.PrivacyType.Types.CUSTOM;
    }
}
