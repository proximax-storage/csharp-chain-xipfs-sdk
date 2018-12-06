using Proximax.Storage.SDK.Exceptions;

namespace Proximax.Storage.SDK.Connections
{
    public enum HttpProtocol
    {
        Http,
        Https
    }

    static class HttpProtocolMethods
    {
        public static string GetProtocol(this HttpProtocol type)
        {
            switch (type)
            {
                case HttpProtocol.Http: return "http";
                case HttpProtocol.Https: return "https";
                default:
                    throw new NetworkTypeInvalidException("Invalid http protocol");
            }
        }
    }

    public static class HttpProtocolConverter
    {
        public static HttpProtocol GetHttpProtocol(string httpProtocol)
        {
            switch (httpProtocol)
            {
                case "HTTP": return HttpProtocol.Http;
                case "HTTPS": return HttpProtocol.Https;
                default:
                    throw new NetworkTypeInvalidException("Invalid protocol");
            }
        }
    }
}