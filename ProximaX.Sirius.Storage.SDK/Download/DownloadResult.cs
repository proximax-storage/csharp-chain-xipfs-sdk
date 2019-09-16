namespace ProximaX.Sirius.Storage.SDK.Download
{
    public class DownloadResult
    {
        public string TransactionHash { get; }
        public int PrivacyType { get; }
        public string Version { get; }
        public DownloadResultData Data { get; }

        private DownloadResult(string transactionHash, int privacyType, string version, DownloadResultData data)
        {
            TransactionHash = transactionHash;
            PrivacyType = privacyType;
            Version = version;
            Data = data;
        }

        internal static DownloadResult Create(string transactionHash, int privacyType, string version,
            DownloadResultData data)
        {
            return new DownloadResult(transactionHash, privacyType, version, data);
        }
    }
}