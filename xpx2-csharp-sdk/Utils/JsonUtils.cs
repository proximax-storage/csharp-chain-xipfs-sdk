using Newtonsoft.Json;

namespace IO.Proximax.SDK.Utils
{
    public static class JsonUtils
    {
        public static T FromJson<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static string ToJson(this object source)
        {
            return JsonConvert.SerializeObject(source);
        }
    }
}
