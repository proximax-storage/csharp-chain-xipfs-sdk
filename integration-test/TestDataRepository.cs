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

            TestDataMap.TryAdd(testMethodName + ".transactionHash", result.TransactionHash);
            TestDataMap.TryAdd(testMethodName + ".dataHash", result.Data.DataHash);
            TestDataMap.TryAdd(testMethodName + ".digest", result.Data.Digest);

            SaveTestDataMap();
        }

        public static string GetData(string testMethodName, string dataName) {
            return TestDataMap.GetValueOrDefault(testMethodName + "." + dataName);
        }

        private static void SaveTestDataMap() {
            lock (TestDataJsonFile)
            {
                StreamWriter streamWriter = null;
                try
                {
                    streamWriter = new StreamWriter(new FileStream(TestDataJsonFile, FileMode.OpenOrCreate));
                    streamWriter.WriteLine(JsonUtils.ToJson(TestDataMap));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                finally
                {
                    streamWriter?.Close();
                }
            }
        }

        private static Dictionary<string, string> LoadTestDataMap() {
            lock (TestDataJsonFile)
            {
                StreamReader streamReader = null;
                try
                {
                    streamReader = new StreamReader(new FileStream(TestDataJsonFile, FileMode.Open));
                    return JsonUtils.FromJson<Dictionary<string, string>>(streamReader.ReadToEndAsync().Result);
                }
                catch (IOException e)
                {
                    Console.WriteLine(e);
                    return new Dictionary<string, string>();
                }
                finally
                {
                    streamReader?.Close();
                }
            }
        }
    }
}