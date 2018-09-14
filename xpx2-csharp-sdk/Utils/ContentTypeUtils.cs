using System;
using System.IO;
using System.Reactive.Linq;
using MimeDetective.Extensions;
using static IO.Proximax.SDK.Utils.ParameterValidationUtils;

//TODO
namespace IO.Proximax.SDK.Utils
{
    public class ContentTypeUtils
    {

        public IObservable<string> DetectContentType(Stream byteStream)
        {
            CheckParameter(byteStream != null, "byteStream is required");

            var contentType = byteStream.GetFileType();
            return Observable.Return(contentType?.Mime);
        }
    }
}
