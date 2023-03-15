using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyncLink.Application.UseCases.Queries.GetById.Group;

namespace SyncLink.Server.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class GroupsController : ControllerBase
{
    private readonly IMediator _mediator;

    public GroupsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("/{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var query = new GetGroupById.Query { Id = id };

        var result = await _mediator.Send(query, cancellationToken);

        return Ok(result);
    }
}