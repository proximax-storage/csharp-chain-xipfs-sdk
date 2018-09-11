using Newtonsoft.Json;

namespace IO.Proximax.SDK.Utils
{
    public static class JsonUtils
    {
        public static T FromJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static string ToJson(object source)
        {
            return JsonConvert.SerializeObject(source);
        }
    }
}
