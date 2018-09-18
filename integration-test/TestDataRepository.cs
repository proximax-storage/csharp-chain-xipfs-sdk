using System;
using System.Collections.Generic;
using System.IO;
using IO.Proximax.SDK.Upload;
using IO.Proximax.SDK.Utils;

namespace IntegrationTests
{
    public static class TestDataRepository
    {
        private const string TestDataJsonFile = @"test_data.json";
        private static readonly Dictionary<string, string> TestDataMap = LoadTestDataMap();

        public static void LogAndSaveResult(UploadResult result, string testMethodName) {
            Console.WriteLine("transaction hash: " + result.TransactionHash);
            Console.WriteLine("data hash: " + result.Data.DataHash);
            Console.WriteLine("data digest: " + result.Data.Digest);

            TestDataMap.Add(testMethodName + ".transactionHash", result.TransactionHash);
            TestDataMap.Add(testMethodName + ".dataHash", result.Data.DataHash);
            TestDataMap.Add(testMethodName + ".digest", result.Data.Digest);

            SaveTestDataMap();
        }

        public static string GetData(string testMethodName, string dataName) {
            return TestDataMap.GetValueOrDefault(testMethodName + "." + dataName);
        }

        private static void SaveTestDataMap() {
            try {
                var streamWriter = new StreamWriter(new FileStream(TestDataJsonFile, FileMode.OpenOrCreate));
                streamWriter.WriteLine(JsonUtils.ToJson(TestDataMap));
                streamWriter.Close();
            } catch (Exception e) {
                Console.WriteLine(e);
            }
        }

        private static Dictionary<string, string> LoadTestDataMap() {
            try {
                var testDataJson = new StreamReader(new FileStream(TestDataJsonFile, FileMode.Open));
                return JsonUtils.FromJson<Dictionary<string, string>>(testDataJson.ReadToEndAsync().Result);
            } catch (IOException e) {
                Console.WriteLine(e);
                return new Dictionary<string, string>();
            }
        }
    }
}