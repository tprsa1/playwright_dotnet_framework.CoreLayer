using Microsoft.Playwright;
using Newtonsoft.Json;
using playwright.dotnet.framework.BusinessLayer.Models.CreateWidgetModels;
using playwright.dotnet.framework.BusinessLayer.Models.UpdateWidgetModels;
using System.Text;

namespace playwright.dotnet.framework.BusinessLayer.APIServiceObject
{
    public class WidgetsServiceObject : BaseServiceObject
    {
        private readonly IAPIRequestContext _context;
        public WidgetsServiceObject(IAPIRequestContext requestContext) {
             _context = requestContext;
        }

        public async Task<IAPIResponse> GetTokenAsync()
        {
            var options = new APIRequestContextOptions();
            options.Headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Authorization", "Basic dWk6dWltYW4=")
            };
            var formData1 = _context.CreateFormData();
            formData1.Set("grant_type", "password");
            formData1.Set("username", "superadmin");
            formData1.Set("password", "IloveMSTest#2");
            // Create form data

            // Set the form data
            options.Form = formData1;

            return await _context.PostAsync($"{BaseUrl}uat/sso/oauth/token", options);
        }

        public async Task<IAPIResponse> GetAllWidgetNames(String token)
        {
            var getOptions = new APIRequestContextOptions();
            getOptions.Headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Authorization", $"bearer {token}")
            };
            return await _context.GetAsync($"{BaseUrl}api/v1/test_automation_mentoring_playwright/widget/names/all", getOptions);
        }
        public async Task<IAPIResponse> GetWidgetById(String token, String Id)
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId + "GetWidgetById" + Id);
            var getOptions = new APIRequestContextOptions();
            getOptions.Headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Authorization", $"bearer {token}")
            };
            return await _context.GetAsync($"{BaseUrl}api/v1/test_automation_mentoring_playwright/widget/{Id}", getOptions);
        }
        public async Task<IAPIResponse> UpdateWidget(String token,String Id, UpdateWidgetRequest updateWidgetRequest)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
            };
            string jsonPayload = JsonConvert.SerializeObject(updateWidgetRequest, settings);
            var getOptions = new APIRequestContextOptions();
            getOptions.Data = jsonPayload;
            getOptions.Headers = new List<KeyValuePair<string, string>>
            {

                new KeyValuePair<string, string>("Authorization", $"bearer {token}"),
                new KeyValuePair<string, string>("Content-Type", "application/json")
            };
            return await _context.PutAsync($"{BaseUrl}api/v1/test_automation_mentoring_playwright/widget/{Id}", getOptions);
        }
        public async Task<IAPIResponse> CreateWidget(String token, CreateWidgetRequestBody createWidgetRequestBody)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
            };
            string jsonPayload = JsonConvert.SerializeObject(createWidgetRequestBody, settings);
            var getOptions = new APIRequestContextOptions();
            getOptions.Data = jsonPayload;
            getOptions.Headers = new List<KeyValuePair<string, string>>
            {

                new KeyValuePair<string, string>("Authorization", $"bearer {token}"),
                new KeyValuePair<string, string>("Content-Type", "application/json")
            };
            var requestBody = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            return await _context.PostAsync($"{BaseUrl}api/v1/test_automation_mentoring_playwright/widget", getOptions);
        }

    }
}
