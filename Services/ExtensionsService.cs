using System.Collections;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Ultra_Saver;
static class ExtensionsService
{
    public static String? getEmailFromClaim(this ClaimsIdentity identity)
    {
        return identity.Claims.FirstOrDefault(o => o.Type == "email")?.Value;
    }


    public static string map(this String str, Func<char, string> fn)
    {
        string result = "";
        for (int i = 0; i < str.Length; ++i)
        {
            result += fn(str[i]);
        }

        return result;
    }

}