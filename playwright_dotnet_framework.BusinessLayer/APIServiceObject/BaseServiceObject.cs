using System.Configuration;
using playwright.dotnet.framework.CoreLayer.Config;

namespace playwright.dotnet.framework.BusinessLayer.APIServiceObject
{
    public class BaseServiceObject
    {
        protected string BaseUrl = ConfigReader.GetValue("AppSettings.LocalEnvironment.BaseUrl");
    }
}
