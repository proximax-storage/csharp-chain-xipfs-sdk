using System;
using System.IO;

namespace IO.Proximax.SDK.Services.Repositories
{
    public abstract class IFileRepository
    {
        public abstract IObservable<string> AddByteStream(Stream byteStream);
        public abstract IObservable<string> AddPath(string path);
        public abstract IObservable<Stream> GetByteStream(string dataHash);
    }
}