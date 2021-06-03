using System.Linq;
using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsPrincipalExtenions
    {
        public static string RetrieveEmailFromPrincipal (this ClaimsPrincipal user)
        {
            // return user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            return user.FindFirstValue(ClaimTypes.Email);
        }
    }
}