using IO.Proximax.SDK.PrivacyStrategies;
using System;
using System.Collections.Generic;
using System.Text;

namespace IO.Proximax.SDK.Download
{
    public class DownloadDataParameter
    {

        public string DataHash { get; }
        public IPrivacyStrategy PrivacyStrategy { get; }
        public string Digest { get; }

        internal DownloadDataParameter(string dataHash, IPrivacyStrategy privacyStrategy, string digest)
        {
            DataHash = dataHash;
            PrivacyStrategy = privacyStrategy;
            Digest = digest;
        }

        public static DownloadDataParameterBuilder Create(string dataHash)
        {
            return new DownloadDataParameterBuilder(dataHash);
        }

    }
}
