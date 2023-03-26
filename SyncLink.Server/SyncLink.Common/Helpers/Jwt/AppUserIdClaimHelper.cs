using System.Security.Claims;

namespace SyncLink.Common.Helpers.Jwt;

public static class AppUserIdClaimHelper
{
    private const string AppUserIdClaimType = nameof(AppUserIdClaimType);

    public static Claim CreateClaim(int userId)
    {
        return new Claim(AppUserIdClaimType, userId.ToString());
    }

    public static int? RetrieveUserId(ClaimsPrincipal claimsPrincipal)
    {
        var rawValue = claimsPrincipal.FindFirst(AppUserIdClaimType)?.Value;

        int.TryParse(rawValue, out var userId);

        return userId;
    }
}