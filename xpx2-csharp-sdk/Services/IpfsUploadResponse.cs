namespace IO.Proximax.SDK.Services
{
    public class IpfsUploadResponse
    {
        public string DataHash { get; }
        public long Timestamp { get; }

        public IpfsUploadResponse(string dataHash, long timestamp)
        {
            DataHash = dataHash;
            Timestamp = timestamp;
        }
    }
}
