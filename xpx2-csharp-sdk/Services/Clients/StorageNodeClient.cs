using System;
using System.IO;
using System.Net.Http;
using System.Reactive.Linq;
using IO.Proximax.SDK.Connections;
using IO.Proximax.SDK.Services.Repositories;
using IO.Proximax.SDK.Utils;
using Newtonsoft.Json;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

namespace IO.Proximax.SDK.Services.Clients
{
    public class StorageNodeClient : IFileRepository
    {
        private const string HeaderCredentials = "HeaderCredentials";
        
        private string ApiUrl { get; set; }
        private string HeaderCredentialsVal { get; set; }

        public StorageNodeClient(StorageConnection storageConnection)
        {
            ApiUrl = storageConnection.RestApiUrl;
            HeaderCredentialsVal = $"NemAddress={storageConnection.NemAddress}; Bearer {storageConnection.BearerToken}";
        }

        public override IObservable<string> AddByteStream(Stream byteStream)
        {
            CheckParameter(byteStream != null, "byteStream is required");

            return Observable.Start(() =>
            {
                using (byteStream)
                {
                    using (var httpClient = new HttpClient())
                    {
                        httpClient.DefaultRequestHeaders.Add(HeaderCredentials, HeaderCredentialsVal);
                        var content = new MultipartFormDataContent {{new StreamContent(byteStream), "file", "file"}};
                        var httpResponseMessage = httpClient.PostAsync(ApiUrl + "/upload/file", content).GetAwaiter().GetResult();
                        var response = httpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        return response.FromJson<UploadFileResponse>().DataHash;
                    }
                }
            });
        }

        public override IObservable<string> AddPath(string path)
        {
            return Observable.Throw<string>(new NotSupportedException("Path upload is not supported on storage"));
        }

        public override IObservable<Stream> GetByteStream(string dataHash)
        {
            CheckParameter(dataHash != null, "dataHash is required");

            return Observable.Start(() =>
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add(HeaderCredentials, HeaderCredentialsVal);
                    return httpClient.GetStreamAsync(ApiUrl + "/download/file").GetAwaiter().GetResult();
                }
            });
        }
        
        public IObservable<NodeInfoResponse> GetNodeInfo()
        {
            return Observable.Start(() =>
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add(HeaderCredentials, HeaderCredentialsVal);
                    return httpClient.GetStringAsync(ApiUrl + "/node/info").GetAwaiter().GetResult().FromJson<NodeInfoResponse>();
                }
            });
        }
        
    }

    internal class UploadFileResponse
    {
        [JsonProperty("dataHash", NullValueHandling=NullValueHandling.Ignore)]
        internal string DataHash { get; }
    }

    public class NodeInfoResponse
    {
        [JsonProperty("blockchainNetwork", NullValueHandling=NullValueHandling.Ignore)]
        internal NodeInfoResponseBlockchainNetwork BlockchainNetwork { get; }
    }
    
    public class NodeInfoResponseBlockchainNetwork
    {
        [JsonProperty("protocol", NullValueHandling=NullValueHandling.Ignore)]
        internal string Protocol { get; }
        [JsonProperty("port", NullValueHandling=NullValueHandling.Ignore)]
        internal int Port { get; }
        [JsonProperty("host", NullValueHandling=NullValueHandling.Ignore)]
        internal string Host { get; }
        [JsonProperty("network", NullValueHandling=NullValueHandling.Ignore)]
        internal string NetworkType { get; }
    }
}