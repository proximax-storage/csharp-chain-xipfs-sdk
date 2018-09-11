using System.Collections.Generic;
using System.IO;
using System.Net;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

namespace IO.Proximax.SDK.Upload
{
    public class UrlResourceParameterData : IByteStreamParameterData
    {
        public string Url { get; }
        
        public UrlResourceParameterData(string url, string description, string name, string contentType, IDictionary<string, string> metadata)
            : base(description, name, contentType, metadata)
        {
            CheckParameter(url != null, "url is required");

            Url = url;
        }

        public override Stream GetByteStream()
        {
            var req = (HttpWebRequest)WebRequest.Create(Url);
            var resp = (HttpWebResponse)req.GetResponse();

            return resp.GetResponseStream();
        }
        
        public static UrlResourceParameterData Create(string url, string description = null, string name = null, 
            string contentType = null, IDictionary<string, string> metadata = null) {
            return new UrlResourceParameterData(url, description, name, contentType, metadata);
        }        
    }
}
