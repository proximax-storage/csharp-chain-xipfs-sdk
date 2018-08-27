using IO.Proximax.SDK.PrivacyStrategies;
using System;
using System.Collections.Generic;
using System.Text;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

namespace IO.Proximax.SDK.Download
{
    public class DownloadParameterBuilder
    {
        public string TransactionHash { get; set; }
        public string RootDataHash { get; set; }
        public IPrivacyStrategy PrivacyStrategy { get; set; }
        public string Digest { get; set; }

        public DownloadParameterBuilder(string transactionHash)
        {
            CheckParameter(transactionHash == null, "transactionHash is required");

            TransactionHash = transactionHash;
        }

        public DownloadParameterBuilder(string rootDataHash, string digest)
        {
            CheckParameter(rootDataHash == null, "rootDataHash is required");

            RootDataHash = rootDataHash;
            Digest = digest;
        }

        public DownloadParameterBuilder WithPrivacyStrategy(IPrivacyStrategy privacyStrategy)
        {
            PrivacyStrategy = privacyStrategy;
            return this;
        }

        public DownloadParameterBuilder WithPlainPrivacy()
        {
            PrivacyStrategy = PlainPrivacyStrategy.Create(null);
            return this;
        }

        public DownloadParameter Build()
        {
            if (this.PrivacyStrategy == null)
                this.PrivacyStrategy = PlainPrivacyStrategy.Create(null);
            return new DownloadParameter(TransactionHash, RootDataHash, PrivacyStrategy, Digest);
        }

    }
}
