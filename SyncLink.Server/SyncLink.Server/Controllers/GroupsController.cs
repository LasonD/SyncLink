using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyncLink.Server.Dtos;

namespace SyncLink.Server.Controllers;

[Authorize]
[ApiController]
public class GroupsController : ControllerBase
{
    [HttpPost("/")]
    public async Task<IActionResult> CreateGroup([FromBody] CreateGroupDto createGroupDto, CancellationToken cancellationToken)
    {

    }
}