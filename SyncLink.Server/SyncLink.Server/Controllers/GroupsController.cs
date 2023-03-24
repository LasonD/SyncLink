using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyncLink.Application.UseCases.Queries.GetById.Group;
using SyncLink.Application.UseCases.Queries.SearchGroups;
using SyncLink.Server.Controllers.Base;

namespace SyncLink.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class GroupsController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public GroupsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var query = new GetGroupById.Query { Id = id };

        var result = await _mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string? searchQuery = null, [FromQuery] bool onlyMembership = false, CancellationToken cancellationToken = default)
    {
        var userId = GetRequiredAppUserId();

        var query = new SearchGroups.Query(userId, searchQuery, onlyMembership);

        var result = await _mediator.Send(query, cancellationToken);

        return Ok(result);
    }
}