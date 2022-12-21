namespace EtsyApiSharp.Infrastructure;

public class DefaultHttpClientFactory : IHttpClientFactory
{
    private static readonly HttpClient Client = new();

    public HttpClient CreateClient(string name) => Client;
}
