using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;

namespace SyncLink.Server.Controllers.Base;

public class ApiControllerBase : ControllerBase
{
    public int? GetAppUserId()
    {
        var value = User.FindFirst(JwtRegisteredClaimNames.NameId)?.Value;

        if (int.TryParse(value, out var userId))
        {
            return userId;
        }

        return null;
    }

    public int GetRequiredAppUserId()
    {
        var userId = GetAppUserId();

        if (userId == null)
        {
            throw new BadHttpRequestException("User id should be specified.");
        }

        return userId.Value;
    }
}