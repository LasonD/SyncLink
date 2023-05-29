using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyncLink.Application.UseCases.Rooms.Commands.CreateRoom;
using SyncLink.Server.Controllers.Base;
using SyncLink.Server.Dtos;

namespace SyncLink.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class RoomsController : ApiControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public RoomsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateRoom(CreateRoomDto createRoom, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateRoom.Command>(createRoom);
        command.UserId = GetRequiredAppUserId();

        var result = await _mediator.Send(command, cancellationToken);

        return Ok(result);
    }
}