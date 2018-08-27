using System;
using System.Collections.Generic;
using System.Text;
using IO.Proximax.SDK.PrivacyStrategies;
using static IO.Proximax.SDK.Models.Constants;

namespace IO.Proximax.SDK.Upload
{
    public class UploadParameter
    {
        public string SignerPrivateKey { get; }
        public string RecipientPublicKey { get; }
        public string Description { get; }
        public IPrivacyStrategy PrivacyStrategy { get; }
        public bool ComputeDigest { get; }
        public string Version { get; }
        public IList<IUploadParameterData> DataList { get; }

        internal UploadParameter(string signerPrivateKey, string recipientPublicKey, string description, IPrivacyStrategy privacyStrategy, bool computeDigest, IList<IUploadParameterData> dataList)
        {
            SignerPrivateKey = signerPrivateKey;
            RecipientPublicKey = recipientPublicKey;
            Description = description;
            PrivacyStrategy = privacyStrategy;
            ComputeDigest = computeDigest;
            DataList = dataList;
            Version = SCHEMA_VERSION;
        }

        public static UploadParameterBuilder Create(string signerPrivateKey, string recipientPublicKey)
        {
            return new UploadParameterBuilder(signerPrivateKey, recipientPublicKey);
        }
    }
}
