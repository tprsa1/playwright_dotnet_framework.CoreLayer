using System.Text;
using Newtonsoft.Json;
using playwright.dotnet.framework.CoreLayer.APIUtils;

public class HttpClientWrapper : IHttpService
{
    private readonly HttpClient _client;

    public HttpClientWrapper()
    {
        _client = new HttpClient();
    }

    public async Task<CustomHttpResponse> GetAsync(string url, Dictionary<string, string> headers)
    {
        AddHeaders(headers);

        HttpResponseMessage response = await _client.GetAsync(url);
        return await CreateCustomHttpResponse(response);
    }

    public async Task<CustomHttpResponse> PostAsync(string url, Dictionary<string, string> headers, object? modelObject)
    {
        AddHeaders(headers);

        StringContent content = SerializeObject(modelObject);
        HttpResponseMessage response = await _client.PostAsync(url, content);
        return await CreateCustomHttpResponse(response);
    }

    public async Task<CustomHttpResponse> PutAsync(string url, Dictionary<string, string> headers, object? modelObject)
    {
        AddHeaders(headers);

        StringContent content = SerializeObject(modelObject);
        HttpResponseMessage response = await _client.PutAsync(url, content);
        return await CreateCustomHttpResponse(response);
    }

    private void AddHeaders(Dictionary<string, string> headers)
    {
        _client.DefaultRequestHeaders.Clear();
        foreach (var header in headers)
        {
            _client.DefaultRequestHeaders.Add(header.Key, header.Value);
        }
    }

    private StringContent SerializeObject(object? modelObject)
    {
        string jsonPayload = JsonConvert.SerializeObject(modelObject);
        return new StringContent(jsonPayload, Encoding.UTF8, "application/json");
    }

    private async Task<CustomHttpResponse> CreateCustomHttpResponse(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();
        var headers = new Dictionary<string, string>();

        foreach (var header in response.Headers)
        {
            headers[header.Key] = string.Join(", ", header.Value);
        }

        foreach (var header in response.Content.Headers)
        {
            headers[header.Key] = string.Join(", ", header.Value);
        }

        return new CustomHttpResponse((int)response.StatusCode, content, headers);
    }

}
