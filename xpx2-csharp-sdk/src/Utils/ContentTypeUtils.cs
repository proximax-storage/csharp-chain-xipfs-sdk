using io.nem2.sdk.Model.Accounts;
using io.nem2.sdk.Model.Blockchain;
using IO.Proximax.SDK.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

//TODO
namespace IO.Proximax.SDK.Utils
{
    public class ContentTypeUtils
    {

        public IObservable<string> DetectContentType(byte[] data, string contentType)
        {
            CheckParameter(data != null, "data is required");

            // TODO
            return Observable.Return<string>(contentType);
        }
    }
}
