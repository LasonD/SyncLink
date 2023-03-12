using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SyncLink.Server.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class GroupsController : ControllerBase
{
    [HttpGet("/{id:int}")]
    [ActionName("test")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        return Ok();
    }
}