using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyncLink.Application.UseCases.Commands.SendMessage;
using SyncLink.Server.Controllers.Base;
using SyncLink.Server.Dtos;

namespace SyncLink.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class MessagesController : ApiControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public MessagesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("")]
    public async Task<IActionResult> SendMessage(SendMessageDto sendMessageData, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<SendMessage.Command>(sendMessageData);

        var result = await _mediator.Send(command, cancellationToken);

        return Ok(result);
    }
}