using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace EtsyApiSharp.Helpers;

public static class AuthHelper
{
    private static readonly Regex CodeVerifierPattern = new(
        "^[A-Za-z0-9._~-]{43,128}$",
        RegexOptions.CultureInvariant);

    public static string CreateCodeVerifier() => Base64UrlEncode(RandomNumberGenerator.GetBytes(32));

    public static string CreateState() => Base64UrlEncode(RandomNumberGenerator.GetBytes(32));

    public static string CreateCodeChallenge(string codeVerifier)
    {
        ValidateCodeVerifier(codeVerifier);

        var codeChallengeBytes = SHA256.HashData(Encoding.ASCII.GetBytes(codeVerifier));
        return Base64UrlEncode(codeChallengeBytes);
    }

    internal static void ValidateCodeVerifier(string codeVerifier)
    {
        if (string.IsNullOrWhiteSpace(codeVerifier) || !CodeVerifierPattern.IsMatch(codeVerifier))
        {
            throw new ArgumentException(
                "The PKCE code verifier must contain 43 to 128 unreserved URI characters (A-Z, a-z, 0-9, '-', '.', '_', or '~').",
                nameof(codeVerifier));
        }
    }

    private static string Base64UrlEncode(byte[] value) => Convert.ToBase64String(value)
        .TrimEnd('=')
        .Replace('+', '-')
        .Replace('/', '_');
}
