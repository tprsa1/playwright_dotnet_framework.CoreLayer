using Microsoft.Playwright;
using Newtonsoft.Json;

namespace playwright.dotnet.framework.CoreLayer.APIUtils
{
    public class PlaywrightHttpWrapper : IHttpService
    {
        private IAPIRequestContext _request;
        private IPlaywright _playwright;
        public PlaywrightHttpWrapper()
        {
            _playwright = Playwright.CreateAsync().Result;
            _request = _playwright.APIRequest.NewContextAsync().Result;
        }

        public async Task<CustomHttpResponse> GetAsync(string url, Dictionary<string, string> headers)
        {
            var getOptions = new APIRequestContextOptions { Headers = headers };
            try
            {
                var response = await _request.GetAsync(url, getOptions);
                var content = await response.TextAsync();
                return new CustomHttpResponse(response.Status, content, response.Headers);
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException("GetAsync PlaywrightWrapper is not working as expected!", exception);
            }
        }

        public async Task<CustomHttpResponse> PostAsync(string url, Dictionary<string, string> headers, object? modelObject)
        {
            try
            {
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                };
                string jsonPayload = JsonConvert.SerializeObject(modelObject, settings);
                var getOptions = new APIRequestContextOptions
                {
                    Headers = headers,
                    Data = jsonPayload
                };
                var response = await _request.PostAsync(url, getOptions);
                var content = await response.TextAsync();
                return new CustomHttpResponse(response.Status, content, response.Headers);
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException("PostAsync PlaywrightWrapper is not working as expected!", exception);
            }
        }

        public async Task<CustomHttpResponse> PutAsync(string url, Dictionary<string, string> headers, object? modelObject)
        {
            try
            {
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                };
                string jsonPayload = JsonConvert.SerializeObject(modelObject, settings);
                var getOptions = new APIRequestContextOptions
                {
                    Headers = headers,
                    Data = jsonPayload
                };
                var response = await _request.PutAsync(url, getOptions);
                var content = await response.TextAsync();
                return new CustomHttpResponse(response.Status, content, response.Headers);
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException("PostAsync PlaywrightWrapper is not working as expected!", exception);
            }
        }
    }


}

