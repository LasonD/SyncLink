using Microsoft.AspNetCore.Mvc;
using SyncLink.Common.Helpers.Jwt;

namespace SyncLink.Server.Controllers.Base;

public class ApiControllerBase : ControllerBase
{
    protected int GetRequiredAppUserId()
    {
        var userId = AppUserIdClaimHelper.RetrieveUserId(User);

        if (userId == null)
        {
            throw new BadHttpRequestException("User id should be specified.");
        }

        return userId.Value;
    }
}