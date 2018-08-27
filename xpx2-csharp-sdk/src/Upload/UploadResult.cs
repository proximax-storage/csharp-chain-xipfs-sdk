using System;
using System.Collections.Generic;
using System.Text;
using IO.Proximax.SDK.Models;

namespace IO.Proximax.SDK.Upload
{
    public class UploadResult
    {
        public string TransactionHash { get; }
        public string Digest { get; }
        public string RootDataHash { get; }
        public ProximaxRootDataModel RootData { get; }

        private UploadResult(string transactionHash, string digest, string rootDataHash, ProximaxRootDataModel rootData)
        {
            TransactionHash = transactionHash;
            Digest = digest;
            RootDataHash = rootDataHash;
            RootData = rootData;
        }

        internal UploadResult Create(string transactionHash, string digest, string rootDataHash, ProximaxRootDataModel rootData)
        {
            return new UploadResult(transactionHash, digest, rootDataHash, rootData);
        }
    }
}
