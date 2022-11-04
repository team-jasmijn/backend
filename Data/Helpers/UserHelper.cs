using System.Security.Claims;

namespace Data.Helpers;

public static class UserHelper
{
    public static int Id(this ClaimsPrincipal user)
    {
        Claim claim = user.Claims.SingleOrDefault(e => e.Type == "Id");
        if (claim == null) return 0;
        return int.TryParse(claim.Value, out int value) ? value : 0;
    }
}