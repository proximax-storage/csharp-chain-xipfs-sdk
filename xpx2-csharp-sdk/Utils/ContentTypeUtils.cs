using System;
using System.IO;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

//TODO
namespace IO.Proximax.SDK.Utils
{
    public class ContentTypeUtils
    {

        public IObservable<string> DetectContentType(Stream byteStream)
        {
            CheckParameter(byteStream != null, "byteStream is required");

            throw new NotImplementedException();
        }
    }
}
