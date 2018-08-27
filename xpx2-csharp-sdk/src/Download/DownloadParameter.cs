using IO.Proximax.SDK.PrivacyStrategies;
using System;
using System.Collections.Generic;
using System.Text;

namespace IO.Proximax.SDK.Download
{
    public class DownloadParameter
    {
        public string TransactionHash { get; }
        public string RootDataHash { get; }
        public IPrivacyStrategy PrivacyStrategy { get; }
        public string Digest { get; }

        internal DownloadParameter(string transactionHash, string rootDataHash, IPrivacyStrategy privacyStrategy, string digest)
        {
            TransactionHash = transactionHash;
            RootDataHash = rootDataHash;
            PrivacyStrategy = privacyStrategy;
            Digest = digest;
        }

        public static DownloadParameterBuilder CreateWithTransactionHash(string transactionHash)
        {
            return new DownloadParameterBuilder(transactionHash);
        }

        public static DownloadParameterBuilder CreateWithRootDataHash(string rootDataHash, string digest)
        {
            return new DownloadParameterBuilder(rootDataHash, digest);
        }

    }
}
