using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyncLink.Application.UseCases.CreateGroup;
using SyncLink.Server.Dtos;

namespace SyncLink.Server.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public UsersController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost("/{id:int}/groups")]
    public async Task<IActionResult> CreateGroup(int id, [FromBody] CreateGroupDto createGroupDto, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateGroup.Command>(createGroupDto);

        command.UserId = id;

        var groupDto = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GroupsController.GetById), nameof(GroupsController), new { id = groupDto.Id }, groupDto);
    }
}