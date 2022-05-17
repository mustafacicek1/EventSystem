using System.Security.Claims;

namespace Webapi_BitirmeProjesi.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetEmail(this ClaimsPrincipal claimsPrincipal)
        {
            var result = claimsPrincipal?.FindFirst(ClaimTypes.Email)?.Value;
            return result;
        }

        public static string GetName(this ClaimsPrincipal claimsPrincipal)
        {
            var result = claimsPrincipal?.FindFirst(ClaimTypes.Name)?.Value;
            return result;
        }
    }
}
