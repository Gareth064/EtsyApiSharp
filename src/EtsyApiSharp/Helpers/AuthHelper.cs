using System.Security.Cryptography;
using System.Text;

namespace EtsyApiSharp.Helpers;

public static class AuthHelper
{
    public static string CreateCodeChallenge(string str)
    {
        using (var sha256 = SHA256.Create())
        {
            var codeChallengeBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(str));
            return Convert.ToBase64String(codeChallengeBytes)
                .Replace('+', '-')
                .Replace("=", "")
                .Replace('/', '_');
        }
    }
}
