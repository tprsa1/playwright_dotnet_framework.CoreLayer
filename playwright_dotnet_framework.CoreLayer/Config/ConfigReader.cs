using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace playwright.dotnet.framework.CoreLayer.Config
{
    public static class ConfigReader
    {
        private static JObject _config;

        static ConfigReader()
        {
            string jsonFilePath = "C:\\Users\\Tomislav_Prsa\\source\\repos\\playwright_dotnet_framework.CoreLayer\\playwright_dotnet_framework.CoreLayer\\appsettings.json";
            string json = File.ReadAllText(jsonFilePath);
            _config = JObject.Parse(json);
        }

        public static string GetValue(string key)
        {
            string[] keys = key.Split('.');
            JToken token = _config;

            foreach (var k in keys)
            {
                token = token[k];
                if (token == null)
                {
                    return null;
                }
            }

            return token.ToString();
        }
    }
}
