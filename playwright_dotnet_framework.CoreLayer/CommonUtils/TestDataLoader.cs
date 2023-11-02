using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace playwright.dotnet.framework.CoreLayer.CommonUtils
{
    public class TestDataLoader
    {
        // Define a static object to act as a lock
        private static readonly object LockObject = new object();

        public static JArray GetTestData(string methodNameSource)
        {
                string jsonFilePath = $"C:\\Users\\Tomislav_Prsa\\source\\repos\\playwright_dotnet_framework.CoreLayer\\playwright_dotnet_framework.BusinessLayer\\bin\\Debug\\net6.0\\TestData\\{methodNameSource}.json";
                string jsonText = File.ReadAllText(jsonFilePath);
                JArray jsonArray = JArray.Parse(jsonText);
                return jsonArray;
        }
    }
}
