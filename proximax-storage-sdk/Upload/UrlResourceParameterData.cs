using System.Collections.Generic;
using System.IO;
using System.Net;
using static Proximax.Storage.SDK.Utils.ParameterValidationUtils;

namespace Proximax.Storage.SDK.Upload
{
    public class UrlResourceParameterData : IByteStreamParameterData
    {
        public string Url { get; }

        public UrlResourceParameterData(string url, string description, string name, string contentType,
            IDictionary<string, string> metadata)
            : base(description, name, contentType, metadata)
        {
            CheckParameter(url != null, "url is required");

            Url = url;
        }

        public override Stream GetByteStream()
        {
            var client = new WebClient();
            return client.OpenRead(Url);
        }

        public static UrlResourceParameterData Create(string url, string description = null, string name = null,
            string contentType = null, IDictionary<string, string> metadata = null)
        {
            return new UrlResourceParameterData(url, description, name, contentType, metadata);
        }
    }
}