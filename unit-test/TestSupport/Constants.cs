using System.Text;

namespace UnitTests.TestSupport
{
    public static class Constants
    {
        public const string TestPrivateKey1 = "8374B5915AEAB6308C34368B15ABF33C79FD7FEFC0DEAF9CC51BA57F120F1190";
        public const string TestPublicKey1 = "9E7930144DA0845361F650BF78A36791ABF2577E251706ECA45480998FE61D18";
        public const string TestAddress1 = "SBI5EBRHONTC6FSO4DNKKBDI7SBUNU4L2W76WUUD";

        public const string TestPrivateKey2 = "369CB3195F88A16F8326DABBD37DA5F8458B55AA5DA6F7E2F756A12BE6CAA546";
        public const string TestPublicKey2 = "8E1A94D534EA6A3B02B0B967701549C21724C7644B2E4C20BF15D01D50097ACB";
        public const string TestAddress2 = "SCW3N4V7LQ4UQBJK3PZGV3YGKMC67LLXYADF765M";

        public const string TestString = "Nam a porta augue. Fusce ut tempus elit, vel vehicula nulla. " +
                                         "Etiam sed est purus. Sed consectetur risus vel velit ultricies gravida. " +
                                         "Ut semper augue massa, vitae dignissim elit luctus vel. " +
                                         "Pellentesque in placerat nisl, interdum rutrum dui. " +
                                         "In ex tortor, condimentum et odio ut, feugiat pretium nibh. " +
                                         "Fusce ac iaculis metus. Duis accumsan ac nunc in maximus. " +
                                         "Quisque varius lobortis tortor, a facilisis nisi convallis ut. " +
                                         "Curabitur vitae consectetur risus, in fermentum magna. " +
                                         "In non lectus interdum massa tempor dapibus. " +
                                         "Etiam eu malesuada turpis, tempor gravida mi. Ut vitae dapibus tellus, at viverra nibh. " +
                                         "Nam bibendum, urna condimentum ultrices dapibus, nisl quam accumsan urna, id feugiat diam ligula rhoncus elit.";

        public static readonly byte[] TestByteArray = Encoding.UTF8.GetBytes(
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
            "Aenean congue sodales massa, non faucibus mauris blandit at. " +
            "Cras vulputate volutpat nulla. Vivamus id nunc vel quam tincidunt finibus eu in justo. " +
            "Proin ut erat vitae dolor rutrum imperdiet sit amet sit amet nibh. " +
            "Aenean pulvinar enim elit, efficitur luctus turpis commodo ac. " +
            "Donec auctor lacus quis velit lacinia, faucibus commodo est bibendum. " +
            "Nulla vitae ante quis ex pretium viverra. Proin sodales felis elit, non imperdiet turpis lacinia sed. " +
            "Cras vitae blandit lectus, et mollis magna. " +
            "Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; " +
            "Vivamus auctor massa sit amet condimentum egestas. " +
            "Morbi arcu nibh, tincidunt nec pretium eu, scelerisque vitae sem. " +
            "Donec commodo dui dapibus, sollicitudin lorem et, efficitur purus.");

        public const string TestPdfFile1 = @"Resources\TestFiles\test_pdf_file_1.pdf";
        public const string TestPdfFile2 = @"Resources\TestFiles\test_pdf_file_2.pdf";
        public const string TestZipFile = @"Resources\TestFiles\test_zip_file.zip";
        public const string TestVideoMp4File = @"Resources\TestFiles\test_large_video.mp4";
        public const string TestVideoMovFile = @"Resources\TestFiles\test_video_file.mov";
        public const string TestAudioMp3File = @"Resources\TestFiles\test_audio_file.mp3";
        public const string TestTextFile = @"Resources\TestFiles\test_text_file.txt";
        public const string TestHtmlFile = @"Resources\TestFiles\test_html_file.html";
        public const string TestImagePngFile = @"Resources\TestFiles\test_image_file.png";
        public const string TestPathFile = @"Resources\TestFiles\TestPath";

        public const string TestPassword = "hkcymenwcxpzkoyowuagcuhvrhavtdcrxbfqganecoxuirxekq";
    }
}