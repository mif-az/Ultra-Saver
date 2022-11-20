using System.Security.Claims;

namespace Ultra_Saver;
public static class ExtensionsService
{
    public static string? getEmailFromClaim(this ClaimsIdentity identity)
    {
        return identity.Claims.FirstOrDefault(o => o.Type == "email")?.Value;
    }


    public static string map(this string str, Func<char, string> fn)
    {
        string result = "";
        for (int i = 0; i < str.Length; ++i)
        {
            result += fn(str[i]);
        }

        return result;
    }


}