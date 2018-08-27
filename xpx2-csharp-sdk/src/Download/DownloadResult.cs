using System;
using System.Collections.Generic;
using System.Text;
using IO.Proximax.SDK.Models;
using static System.Linq.Enumerable;

namespace IO.Proximax.SDK.Download
{
    public class DownloadResult
    {
        public int PrivacyType { get; }
        public string PrivacySearchTag { get; }
        public string Description { get; }
        public string Version { get; }
        public IList<DownloadResultData> DataList { get; }

        internal DownloadResult(int privacyType, string privacySearchTag, string description, string version, IList<DownloadResultData> dataList)
        {
            PrivacyType = privacyType;
            PrivacySearchTag = privacySearchTag;
            Description = description;
            Version = version;
            DataList = dataList;
        }

        internal static DownloadResult Create(ProximaxRootDataModel rootData, IList<byte[]> decryptedDataList)
        {
            var dataList = rootData.DataList;
            var downloadResultDataList = Range(0, dataList.Count).Select(index => new DownloadResultData(decryptedDataList[index],
                dataList[index].Description, dataList[index].Name, dataList[index].ContentType, dataList[index].Metadata)).ToList();

            return new DownloadResult(rootData.PrivacyType, rootData.PrivacySearchTag, rootData.Description, rootData.Version, downloadResultDataList);
        }
    }
}
