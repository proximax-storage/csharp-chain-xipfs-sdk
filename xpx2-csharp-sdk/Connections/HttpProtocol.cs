using IO.Proximax.SDK.Exceptions;

namespace IO.Proximax.SDK.Connections
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
}