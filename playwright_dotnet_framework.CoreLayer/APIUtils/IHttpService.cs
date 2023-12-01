
namespace playwright.dotnet.framework.CoreLayer.APIUtils
{
    public interface IHttpService
    {
        Task<CustomHttpResponse> GetAsync(string url, Dictionary<string, string> headers);
        Task<CustomHttpResponse> PostAsync(string url, Dictionary<string, string> headers, object? modelObject);
        Task<CustomHttpResponse> PutAsync(string url, Dictionary<string, string> headers, object? modelObject);
    }
}
