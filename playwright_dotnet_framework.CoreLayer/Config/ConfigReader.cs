using Newtonsoft.Json.Linq;

namespace playwright.dotnet.framework.CoreLayer.Config
{
    public static class ConfigReader
    {
        private static JObject _config;

        static ConfigReader()
        {
            string jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
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
