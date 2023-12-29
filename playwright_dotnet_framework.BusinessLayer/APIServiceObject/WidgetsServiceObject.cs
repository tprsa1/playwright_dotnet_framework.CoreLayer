using Microsoft.Playwright;
using playwright.dotnet.framework.BusinessLayer.Models.CreateWidgetModels;
using playwright.dotnet.framework.BusinessLayer.Models.UpdateWidgetModels;
using playwright.dotnet.framework.CoreLayer.APIUtils;

namespace playwright.dotnet.framework.BusinessLayer.APIServiceObject
{
    public class WidgetsServiceObject : BaseServiceObject
    {
        private readonly IHttpService _httpService;
        public WidgetsServiceObject(IHttpService httpService)
        {
            _httpService = httpService;
        }


        public async Task<CustomHttpResponse> GetWidgetById(string? Id)
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId + "GetWidgetById" + Id);
            var getOptions = new APIRequestContextOptions();
            var headers = new Dictionary<string, string>
            {
                { "Authorization", $"bearer {apiKey}" }
            };
            return await _httpService.GetAsync($"{BaseUrl}api/v1/tomislav_prsa_personal/widget/{Id}", headers);
        }
        public async Task<CustomHttpResponse> UpdateWidget(string? Id, UpdateWidgetRequest updateWidgetRequest)
        {
            var getOptions = new APIRequestContextOptions();
            var headers = new Dictionary<string, string>
            {

                { "Authorization", $"bearer {apiKey}" },
                { "Content-Type", "application/json" }
            };
            return await _httpService.PutAsync($"{BaseUrl}api/v1/tomislav_prsa_personal/widget/{Id}", headers, updateWidgetRequest);
        }
        public async Task<CustomHttpResponse> CreateWidget(CreateWidgetRequestBody? createWidgetRequestBody)
        {
            var getOptions = new APIRequestContextOptions();
            var headers = new Dictionary<string, string>
            {

                { "Authorization", $"bearer {apiKey}" },
                { "Content-Type", "application/json" }
            };
            return await _httpService.PostAsync($"{BaseUrl}api/v1/tomislav_prsa_personal/widget", headers, createWidgetRequestBody);
        }

    }
}
