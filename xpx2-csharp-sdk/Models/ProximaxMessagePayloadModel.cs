using Newtonsoft.Json;

namespace IO.Proximax.SDK.Models
{
    public class ProximaxMessagePayloadModel
    {
        [JsonProperty("privacyType")]
        public int PrivacyType { get; }
        [JsonProperty("version")]
        public string Version { get; }
        [JsonProperty("data")]
        public ProximaxDataModel Data { get; }

        public ProximaxMessagePayloadModel(int privacyType, string version, ProximaxDataModel data)
        {
            PrivacyType = privacyType;
            Version = version;
            Data = data;
        }
        
        public static ProximaxMessagePayloadModel Create(int privacyType, string version, ProximaxDataModel data) {
            return new ProximaxMessagePayloadModel(privacyType, version, data);
        }
    }
}
