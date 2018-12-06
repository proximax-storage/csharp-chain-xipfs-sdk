namespace Proximax.Storage.SDK.Services
{
    public class FileUploadResponse
    {
        public string DataHash { get; }
        public long Timestamp { get; }
        public string Digest { get; }

        public FileUploadResponse(string dataHash, long timestamp, string digest = null)
        {
            DataHash = dataHash;
            Timestamp = timestamp;
            Digest = digest;
        }
    }
}