using System;
using System.IO;
using Proximax.Storage.SDK.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static UnitTests.TestSupport.Constants;

namespace UnitTests.Utils
{
    [TestClass]
    public class StreamUtilsTests
    {
        [TestMethod, Timeout(10000)]
        public void ShouldSaveToFile()
        {
            var tempFileName = Path.GetTempPath() + CurrentTimeMillis() + "-test.txt";

            new FileStream(TestTextFile, FileMode.Open, FileAccess.Read).SaveToFile(tempFileName);

            var expected = new FileStream(TestTextFile, FileMode.Open, FileAccess.Read).GetContentAsString();
            var actual = new FileStream(tempFileName, FileMode.Open, FileAccess.Read).GetContentAsString();

            Assert.AreEqual(expected, actual);
        }

        private static long CurrentTimeMillis()
        {
            return (long) (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds;
        }
    }
}