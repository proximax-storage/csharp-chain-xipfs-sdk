﻿using System;
using Ipfs.Api;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

namespace IO.Proximax.SDK.Connections
{
    public class IpfsConnection : IFileStorageConnection
    {
        public IpfsClient Ipfs { get; }
        public string RestApiUrl { get; }
        public string ApiHost { get; }
        public int ApiPort { get; }
        public HttpProtocol HttpProtocol { get; }

        public IpfsConnection(string apiHost = "localhost", int apiPort = 5001, HttpProtocol apiProtocol = HttpProtocol.Http)
        {
            CheckParameter(apiHost != null, "apiHost is required");
            CheckParameter(apiPort > 0, "apiPort must be non-negative int");
            CheckParameter(apiProtocol != null, "apiProtocol is required");

            ApiHost = apiHost;
            ApiPort = apiPort;
            HttpProtocol = apiProtocol;
            RestApiUrl = new UriBuilder(HttpProtocol.GetProtocol(), apiHost, apiPort).Uri.AbsoluteUri;
            Ipfs = new IpfsClient(RestApiUrl);
        }
    }
}