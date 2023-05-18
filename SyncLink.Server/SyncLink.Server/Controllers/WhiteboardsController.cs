using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyncLink.Application.UseCases.Commands.CreateRoom;
using SyncLink.Server.Controllers.Base;
using SyncLink.Server.Dtos;

namespace SyncLink.Server.Controllers;

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

    [HttpPost("")]
    public async Task<IActionResult> CreateWhiteboard(int groupId, CreateRoomDto createRoom, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateRoom.Command>(createRoom);
        command.UserId = GetRequiredAppUserId();
        command.GroupId = groupId;

        var result = await _mediator.Send(command, cancellationToken);

        return Ok(result);
    }
}