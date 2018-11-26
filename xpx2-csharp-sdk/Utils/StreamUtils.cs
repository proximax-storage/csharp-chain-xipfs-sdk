namespace IO.Proximax.SDK.Utils
{
    public static class StreamUtils
    {
        public static byte[] ReadExactly(this System.IO.Stream stream, int count)
        {
            var buffer = new byte[count];
            var offset = 0;
            while (offset < count)
            {
                var read = stream.Read(buffer, offset, count - offset);
                if (read == 0)
                    throw new System.IO.EndOfStreamException();
                offset += read;
            }
            System.Diagnostics.Debug.Assert(offset == count);
            return buffer;
        }
    }
}