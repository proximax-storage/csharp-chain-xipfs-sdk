using System;
using System.IO;

namespace UnitTests.TestSupport
{
    public static class FileHelper
    {

        public static string FileUrlFromRelativePath(string filePath)
        {
            return new Uri(Directory.GetCurrentDirectory() + @"\" + filePath).AbsoluteUri;
        }
        
    }
}