using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyncLink.Application.Contracts.Data.Enums;
using SyncLink.Application.UseCases.Commands.CreateGroup;
using SyncLink.Application.UseCases.Queries.GetById.Group;
using SyncLink.Application.UseCases.Queries.SearchGroups;
using SyncLink.Server.Controllers.Base;
using SyncLink.Server.Dtos;

namespace SyncLink.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class GroupsController : ApiControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GroupsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, bool isComplete = true, CancellationToken cancellationToken = default)
    {
        var query = new GetGroupById.Query { Id = id };

        var result = await _mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateGroup([FromBody] CreateGroupDto createGroupDto, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateGroup.Command>(createGroupDto);

        command.UserId = GetRequiredAppUserId();

        var groupDto = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction("GetById", "Groups", new { id = groupDto.Id }, groupDto);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string? searchQuery = null, [FromQuery] GroupSearchMode groupSearchMode = GroupSearchMode.Membership, CancellationToken cancellationToken = default)
    {
        var userId = GetRequiredAppUserId();

        var query = new SearchGroups.Query(userId, searchQuery, groupSearchMode);

        var result = await _mediator.Send(query, cancellationToken);

        return Ok(result);
    }
}