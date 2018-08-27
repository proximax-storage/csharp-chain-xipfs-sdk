using io.nem2.sdk.Infrastructure.Buffers.Model;
using io.nem2.sdk.Infrastructure.HttpRepositories;
using io.nem2.sdk.Infrastructure.Listeners;
using io.nem2.sdk.Model.Accounts;
using io.nem2.sdk.Model.Transactions;
using IO.Proximax.SDK.Connections;
using IO.Proximax.SDK.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

//TODO
namespace IO.Proximax.SDK.Services.Clients
{
    public class IpfsClient
    {
        private IpfsConnection IpfsConnection { get; set; }

        public IpfsClient(IpfsConnection ipfsConnection)
        {
            IpfsConnection = ipfsConnection;
        }

        public IObservable<String> AddByteArray(byte[] data)
        {
            CheckParameter(data != null, "data is required");

            throw new NotImplementedException();
        }

        /*
        public IObservable<String> AddPath(File path)
        {
            CheckParameter(path != null, "path is required");
            CheckParameter(path.isDirectory(), "path should be directory/folder");

            throw new NotImplementedException();
        }
        */

        public IObservable<IList<string>> Pin(string dataHash)
        {
            CheckParameter(dataHash != null, "dataHash is required");

            throw new NotImplementedException();
        }

        public IObservable<byte[]> Get(string dataHash)
        {
            CheckParameter(dataHash != null, "dataHash is required");

            throw new NotImplementedException();
        }
    }
}
