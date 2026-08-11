using System;

namespace BlazorTestApp.Data;

public static class AppState
{
    public static long ShopId { get; set; }
    public static string CodeVerifier { get; set; }
    public static string OAuthState { get; set; }
    private static string _accessToken;
    public static string TokenResponse
    {
        get => _accessToken;
        set
        {
            _accessToken = value;
            NotifyStateHasChanged();
        }
    }

    private static string _authorizationCode;

    public static string AuthorizationCode
    {
        get => _authorizationCode;
        set
        {
            _authorizationCode = value;
            NotifyStateHasChanged();
        }
    }

    public static event Action OnChange;
    private static void NotifyStateHasChanged()
    {
        OnChange?.Invoke();
    }
}
