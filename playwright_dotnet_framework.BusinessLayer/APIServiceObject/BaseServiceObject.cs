using playwright.dotnet.framework.CoreLayer.Config;

namespace playwright.dotnet.framework.BusinessLayer.APIServiceObject
{
    public class BaseServiceObject
    {
        protected string BaseUrl = ConfigReader.GetValue("AppSettings.DevEnvironment.BaseUrl");
        protected string apiKey = ConfigReader.GetValue("AppSettings.DevEnvironment.apiKey");
    }
}
