using IO.Proximax.SDK.PrivacyStrategies;
using System;
using System.Collections.Generic;
using System.Text;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

namespace IO.Proximax.SDK.Download
{
    public class DownloadDataParameterBuilder
    {
        private string DataHash { get; set; }
        private IPrivacyStrategy PrivacyStrategy { get; set; }
        private string Digest { get; set; }

        public DownloadDataParameterBuilder(string dataHash)
        {
            CheckParameter(dataHash == null, "dataHash is required");

            DataHash = dataHash;
        }

        public DownloadDataParameterBuilder WithDigest(string digest)
        {
            Digest = digest;
            return this;
        }

        public DownloadDataParameterBuilder WithPrivacyStrategy(IPrivacyStrategy privacyStrategy)
        {
            PrivacyStrategy = privacyStrategy;
            return this;
        }

        public DownloadDataParameterBuilder WithPlainPrivacy()
        {
            PrivacyStrategy = PlainPrivacyStrategy.Create(null);
            return this;
        }

        public DownloadDataParameter Build()
        {
            if (this.PrivacyStrategy == null)
                this.PrivacyStrategy = PlainPrivacyStrategy.Create(null);
            return new DownloadDataParameter(DataHash, PrivacyStrategy, Digest);
        }

    }
}
