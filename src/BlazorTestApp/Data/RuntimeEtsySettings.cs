namespace BlazorTestApp.Data;

/// <summary>
/// Holds Etsy credentials for the current test-app process only. Values are never persisted.
/// </summary>
public sealed class RuntimeEtsySettings
{
    private const string PlaceholderClientId = "runtime-settings-required";
    private const string PlaceholderSharedSecret = "runtime-settings-required";
    private readonly object syncRoot = new();
    private string clientId = string.Empty;
    private string sharedSecret = string.Empty;

    public event Action? Changed;

    public bool IsConfigured
    {
        get
        {
            lock (syncRoot)
                return !string.IsNullOrWhiteSpace(clientId) && !string.IsNullOrWhiteSpace(sharedSecret);
        }
    }

    public string ClientId
    {
        get
        {
            lock (syncRoot)
                return clientId;
        }
    }

    public string ClientIdForService => IsConfigured ? ClientId : PlaceholderClientId;

    public string SharedSecretForService
    {
        get
        {
            lock (syncRoot)
                return string.IsNullOrWhiteSpace(sharedSecret) ? PlaceholderSharedSecret : sharedSecret;
        }
    }

    public string RedirectUri => "https://localhost:5001/secret/callback";

    public void Configure(string newClientId, string newSharedSecret)
    {
        if (string.IsNullOrWhiteSpace(newClientId))
            throw new ArgumentException("An Etsy API keystring is required.", nameof(newClientId));

        if (string.IsNullOrWhiteSpace(newSharedSecret))
            throw new ArgumentException("An Etsy API shared secret is required.", nameof(newSharedSecret));

        lock (syncRoot)
        {
            clientId = newClientId.Trim();
            sharedSecret = newSharedSecret.Trim();
        }

        Changed?.Invoke();
    }

    public void Clear()
    {
        lock (syncRoot)
        {
            clientId = string.Empty;
            sharedSecret = string.Empty;
        }

        Changed?.Invoke();
    }

}
