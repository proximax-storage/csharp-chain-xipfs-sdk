using IO.Proximax.SDK.Models;

namespace IO.Proximax.SDK.Upload
{
    public class UploadResult
    {
        public string TransactionHash { get; }
        public int PrivacyType { get; }
        public string Version { get; }
        public ProximaxDataModel Data { get; }

        private UploadResult(string transactionHash, int privacyType, string version, ProximaxDataModel data)
        {
            TransactionHash = transactionHash;
            PrivacyType = privacyType;
            Version = version;
            Data = data;
        }

        internal static UploadResult Create(string transactionHash, int privacyType, string version, ProximaxDataModel data)
        {
            return new UploadResult(transactionHash, privacyType, version, data);
        }
    }
}
