using System;
using System.Collections.Generic;
using System.Text;

namespace IO.Proximax.SDK.Models
{
    public class ProximaxMessagePayloadModel
    {
        public string Digest { get; }
        public string RootDataHash { get; }
        public int PrivacyType { get; }
        public string PrivacySearchTag { get; }
        public string Description { get; }
        public string Version { get; }

        ProximaxMessagePayloadModel(string digest, string rootDataHash, int privacyType, string privacySearchTag,
            string description, string version)
        {
            Digest = digest;
            RootDataHash = rootDataHash;
            PrivacyType = privacyType;
            PrivacySearchTag = privacySearchTag;
            Description = description;
            Version = version;
        }

        public static ProximaxMessagePayloadModel Create(string rootDataHash, string digest, string description,
            int privacyType, string privacySearchTag, string version)
        {
            return new ProximaxMessagePayloadModel(digest, rootDataHash, privacyType, privacySearchTag, description, version);
        }
    }
}
