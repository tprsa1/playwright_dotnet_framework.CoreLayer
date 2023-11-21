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


        public async Task<IAPIResponse> GetAllWidgetNames()
        {
            var getOptions = new APIRequestContextOptions();
            getOptions.Headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Authorization", $"bearer {apiKey}")
            };
            return await _context.GetAsync($"{BaseUrl}api/v1/tomislav_prsa_personal/widget/names/all", getOptions);
        }
        public async Task<IAPIResponse> GetWidgetById(string? Id)
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId + "GetWidgetById" + Id);
            var getOptions = new APIRequestContextOptions();
            getOptions.Headers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Authorization", $"bearer {apiKey}")
            };
            return await _context.GetAsync($"{BaseUrl}api/v1/tomislav_prsa_personal/widget/{Id}", getOptions);
        }
        public async Task<IAPIResponse> UpdateWidget(string? Id, UpdateWidgetRequest updateWidgetRequest)
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

                new KeyValuePair<string, string>("Authorization", $"bearer {apiKey}"),
                new KeyValuePair<string, string>("Content-Type", "application/json")
            };
            return await _context.PutAsync($"{BaseUrl}api/v1/tomislav_prsa_personal/widget/{Id}", getOptions);
        }
        public async Task<IAPIResponse> CreateWidget(CreateWidgetRequestBody createWidgetRequestBody)
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

                new KeyValuePair<string, string>("Authorization", $"bearer {apiKey}"),
                new KeyValuePair<string, string>("Content-Type", "application/json")
            };
            var requestBody = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            return await _context.PostAsync($"{BaseUrl}api/v1/tomislav_prsa_personal/widget", getOptions);
        }

    }
}
