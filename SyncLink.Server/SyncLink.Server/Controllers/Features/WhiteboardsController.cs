using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyncLink.Application.UseCases.Features.Whiteboard.Commands;
using SyncLink.Application.UseCases.Features.Whiteboard.Queries;
using SyncLink.Server.Controllers.Base;
using SyncLink.Server.Dtos;

namespace SyncLink.Server.Controllers.Features;

[Authorize]
[ApiController]
[Route("api/groups/{groupId}/features/[controller]")]
public class WhiteboardsController : ApiControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public WhiteboardsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("{whiteboardId}")]
    public async Task<IActionResult> GetWhiteboard(int groupId, int whiteboardId, CancellationToken cancellationToken)
    {
        var query = new GetWhiteboardById.Query
        {
            GroupId = groupId,
            Id = whiteboardId,
            UserId = GetRequiredAppUserId(),
        };

        var result = await _mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpGet("")]
    public async Task<IActionResult> GetWhiteboards(int groupId, int pageNumber = 1, int pageSize = int.MaxValue, CancellationToken cancellationToken = default)
    {
        var query = new GetWhiteboards.Query
        {
            GroupId = groupId,
            PageNumber = pageNumber,
            PageSize = pageSize,
            UserId = GetRequiredAppUserId(),
        };

        var result = await _mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateWhiteboard(int groupId, CreateWhiteboardDto createWhiteboard, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateWhiteboard.Command>(createWhiteboard);
        command.UserId = GetRequiredAppUserId();
        command.GroupId = groupId;

        var result = await _mediator.Send(command, cancellationToken);

        return Ok(result);
    }
}