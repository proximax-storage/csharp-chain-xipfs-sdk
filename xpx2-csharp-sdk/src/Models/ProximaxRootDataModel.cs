using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace IO.Proximax.SDK.Models
{
    public class ProximaxRootDataModel
    {
        public int PrivacyType { get; }
        public string PrivacySearchTag { get; }
        public string Description { get; }
        public string Version { get; }
        public IList<ProximaxDataModel> DataList { get; }

        public ProximaxRootDataModel(int privacyType, string privacySearchTag, string description, string version,
            IList<ProximaxDataModel> dataList)
        {
            PrivacyType = privacyType;
            PrivacySearchTag = privacySearchTag;
            Description = description;
            Version = version;
            DataList = dataList == null ? ImmutableList<ProximaxDataModel>.Empty : dataList.ToImmutableList();
        }
    }
}
