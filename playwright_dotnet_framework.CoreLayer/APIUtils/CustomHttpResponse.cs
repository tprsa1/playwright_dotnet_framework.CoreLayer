
namespace playwright.dotnet.framework.CoreLayer.APIUtils
{
    public class CustomHttpResponse
    {
        public int StatusCode { get; set; }
        public string Content { get; set; }
        public Dictionary<string, string> Headers { get; set; }

        public CustomHttpResponse(int statusCode, string content, Dictionary<string, string> headers)
        {
            StatusCode = statusCode;
            Content = content;
            Headers = headers;
        }
    }
}
