using Newtonsoft.Json.Linq;

namespace playwright.dotnet.framework.CoreLayer.CommonUtils
{
    public class TestDataLoader
    {
        // Define a static object to act as a lock
        private static readonly object LockObject = new object();

        public static JArray GetTestData(string methodNameSource)
        {
            string jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), $"TestData/{methodNameSource}.json"); ;
                string jsonText = File.ReadAllText(jsonFilePath);
                JArray jsonArray = JArray.Parse(jsonText);
                return jsonArray;
        }
    }
}
