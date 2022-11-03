using System.Security.Claims;

namespace Data.Helpers;

public static class UserHelper
{
    public static int Id(this ClaimsPrincipal user)
    {
        Claim claimn = user.Claims.SingleOrDefault(e => e.Type == "Id");
        if (claimn == null) return 0;
        return int.TryParse(claimn.Value, out int value) ? value : 0;
    }
}