using Newtonsoft.Json;

namespace ProximaX.Sirius.Storage.SDK.Models
{
    public class ProximaxMessagePayloadModel
    {
        [JsonProperty("privacyType", NullValueHandling = NullValueHandling.Ignore)]
        public int PrivacyType { get; }

        [JsonProperty("version", NullValueHandling = NullValueHandling.Ignore)]
        public string Version { get; }

        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public ProximaxDataModel Data { get; }

        public ProximaxMessagePayloadModel(int privacyType, string version, ProximaxDataModel data)
        {
            PrivacyType = privacyType;
            Version = version;
            Data = data;
        }

        public static ProximaxMessagePayloadModel Create(int privacyType, string version, ProximaxDataModel data)
        {
            return new ProximaxMessagePayloadModel(privacyType, version, data);
        }
    }
}