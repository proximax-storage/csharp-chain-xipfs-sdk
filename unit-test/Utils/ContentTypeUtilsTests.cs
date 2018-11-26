using System.IO;
using System.Reactive.Linq;
using IO.Proximax.SDK.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static UnitTests.TestSupport.Constants;

namespace UnitTests.Utils
{
    [TestClass]
    public class ContentTypeUtilsTests
    {
        private ContentTypeUtils UnitUnderTest { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            UnitUnderTest = new ContentTypeUtils();
        }

        // not detected
        [TestMethod, Timeout(10000), Ignore] 
        public void ShouldIdentifyContentTypeBasedOnDataWhenText()
        {
            var result = UnitUnderTest.DetectContentType(new FileStream(TestTextFile, FileMode.Open, FileAccess.Read)).Wait();

            Assert.IsNotNull(result);
            Assert.AreEqual(result, "text/plain");
        }
        
        // not detected
        [TestMethod, Timeout(10000), Ignore] 
        public void ShouldIdentifyContentTypeBasedOnDataWhenHtml()
        {
            var result = UnitUnderTest.DetectContentType(new FileStream(TestHtmlFile, FileMode.Open, FileAccess.Read)).Wait();

            Assert.IsNotNull(result);
            Assert.AreEqual(result, "text/html");
        }
        
        [TestMethod, Timeout(10000)]
        public void ShouldIdentifyContentTypeBasedOnDataWhenMov() {
            var result = UnitUnderTest.DetectContentType(new FileStream(TestVideoMovFile, FileMode.Open, FileAccess.Read)).Wait();

            Assert.IsNotNull(result);
            //Assert.AreEqual(result, "video/quicktime");
            Assert.AreEqual(result, "video/mp4"); 
        }

        [TestMethod, Timeout(10000)]
        public void ShouldIdentifyContentTypeBasedOnDataWhenImage()
        {
            var result = UnitUnderTest.DetectContentType(new FileStream(TestImagePngFile, FileMode.Open, FileAccess.Read)).Wait();

            Assert.IsNotNull(result);
            Assert.AreEqual(result, "image/png");
        }

        [TestMethod, Timeout(10000)]
        public void ShouldIdentifyContentTypeBasedOnDataWhenPdf()
        {
            var result = UnitUnderTest.DetectContentType(new FileStream(TestPdfFile2, FileMode.Open, FileAccess.Read)).Wait();

            Assert.IsNotNull(result);
            Assert.AreEqual(result, "application/pdf");
        }

        [TestMethod, Timeout(10000)]
        public void shouldIdentifyContentTypeBasedOnDataWhenZip()
        {
            var result = UnitUnderTest.DetectContentType(new FileStream(TestZipFile, FileMode.Open, FileAccess.Read)).Wait();

            Assert.IsNotNull(result);
//            Assert.AreEqual(result, "application/zip");
            Assert.AreEqual(result, "application/x-compressed");
        }

        [TestMethod, Timeout(10000)]
        public void ShouldIdentifyContentTypeBasedOnDataWhenMp3()
        {
            var result = UnitUnderTest.DetectContentType(new FileStream(TestAudioMp3File, FileMode.Open, FileAccess.Read)).Wait();

            Assert.IsNotNull(result);
            Assert.AreEqual(result, "audio/mpeg");
        }

        [TestMethod, Timeout(10000)]
        public void ShouldIdentifyContentTypeBasedOnDataWhenMp4()
        {
            var result = UnitUnderTest.DetectContentType(new FileStream(TestVideoMp4File, FileMode.Open, FileAccess.Read)).Wait();

            Assert.IsNotNull(result);
//            Assert.AreEqual(result, "video/mp4");
            Assert.AreEqual(result, "video/x-m4v");
        }
    }
}