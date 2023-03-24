using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyncLink.Application.UseCases.Commands.CreateGroup;
using SyncLink.Server.Controllers.Base;
using SyncLink.Server.Dtos;

namespace SyncLink.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UsersController : ApiControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public UsersController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost("{id:int}/groups")]
    public async Task<IActionResult> CreateGroup(int id, [FromBody] CreateGroupDto createGroupDto, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateGroup.Command>(createGroupDto);

        command.UserId = id;

        var groupDto = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction("GetById", "Groups", new { id = groupDto.Id }, groupDto);
    }
}