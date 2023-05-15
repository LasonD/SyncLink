﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyncLink.Application.Contracts.Data.Enums;
using SyncLink.Application.Domain;
using SyncLink.Application.UseCases.Commands.CreateGroup;
using SyncLink.Application.UseCases.Queries;
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
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken = default)
    {
        var query = new GetGroupById.Query
        {
            Id = id,
            UserId = GetRequiredAppUserId()
        };

        var result = await _mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpGet("{groupId:int}/rooms/{roomId:int}")]
    public async Task<IActionResult> GetRoomOfGroupById(int groupId, int roomId, CancellationToken cancellationToken = default)
    {
        var query = new GetRoom.Query
        {
            UserId = GetRequiredAppUserId(),
            GroupId = groupId,
            RoomId = roomId,
        };

        var result = await _mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpGet("{groupId:int}/rooms/{roomId:int}/messages")]
    public async Task<IActionResult> GetRoomMessages(int groupId, int roomId, [FromQuery] int pageNumber, [FromQuery] int pageSize, CancellationToken cancellationToken = default)
    {
        var query = new GetMessages.Query
        {
            UserId = GetRequiredAppUserId(),
            GroupId = groupId,
            RoomId = roomId,
            PageSize = pageSize,
            PageNumber = pageNumber
        };

        var result = await _mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpGet("{groupId:int}/members/{otherUserId:int}/messages")]
    public async Task<IActionResult> GetPrivateMessages(int groupId, int otherUserId, [FromQuery] int pageNumber, [FromQuery] int pageSize, CancellationToken cancellationToken = default)
    {
        var query = new GetMessages.Query
        {
            UserId = GetRequiredAppUserId(),
            GroupId = groupId,
            OtherUserId = otherUserId,
            PageSize = pageSize,
            PageNumber = pageNumber
        };

        var result = await _mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpGet("{groupId:int}/rooms/{roomId:int}/members")]
    public async Task<IActionResult> GetRoomMembers(int groupId, int roomId, [FromQuery] int pageNumber, [FromQuery] int pageSize, CancellationToken cancellationToken = default)
    {
        var query = new GetRoomMembers.Query
        {
            UserId = GetRequiredAppUserId(),
            GroupId = groupId,
            RoomId = roomId,
            PageSize = pageSize,
            PageNumber = pageNumber
        };

        var result = await _mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpGet("{groupId:int}/members/{memberId:int}/private")]
    public async Task<IActionResult> GetPrivateRoom(int groupId, int memberId, CancellationToken cancellationToken = default)
    {
        var query = new GetRoom.Query
        {
            UserId = GetRequiredAppUserId(),
            GroupId = groupId,
            UserIdForPrivateRoom = memberId,
        };

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

    [HttpGet("{id}/members")]
    public async Task<IActionResult> GetGroupUsers(int id, [FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1, CancellationToken cancellationToken = default)
    {
        var userId = GetRequiredAppUserId();

        var query = new GetGroupMembers.Query
        {
            GroupId = id,
            PageNumber = pageNumber,
            PageSize = pageSize,
            UserId = userId
        };
    
        var result = await _mediator.Send(query, cancellationToken);
    
        return Ok(result);
    }

    [HttpGet("{groupId:int}/rooms")]
    public async Task<IActionResult> GetRooms(int groupId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 25, CancellationToken cancellationToken = default)
    {
        var query = new GetRoomMembers.Query
        {
            UserId = GetRequiredAppUserId(),
            GroupId = groupId,
            RoomId = roomId,
            PageSize = pageSize,
            PageNumber = pageNumber
        };

        var result = await _mediator.Send(query, cancellationToken);

        return Ok(result);
    }
}