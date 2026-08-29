namespace EtsyApiSharp.Infrastructure;
/// <summary>
/// Represents Default Http Client Factory.
/// </summary>

public class DefaultHttpClientFactory : IHttpClientFactory
{
    private static readonly HttpClient Client = new();
    /// <summary>
    /// Executes the Create Client operation.
    /// </summary>

    public HttpClient CreateClient(string name) => Client;
}
