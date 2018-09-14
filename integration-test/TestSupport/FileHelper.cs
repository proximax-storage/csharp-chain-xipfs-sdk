using System;
using System.IO;

namespace IntegrationTests.TestSupport
{
    public static class FileHelper
    {

        public static string FileUrlFromRelativePath(string filePath)
        {
            return new Uri(Directory.GetCurrentDirectory() + @"\" + filePath).AbsoluteUri;
        }
        
    }
}