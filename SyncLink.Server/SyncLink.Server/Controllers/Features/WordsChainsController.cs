using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyncLink.Application.UseCases.Features.TextChainGame.Commands;
using SyncLink.Application.UseCases.Features.TextChainGame.Queries;
using SyncLink.Server.Controllers.Base;

namespace SyncLink.Server.Controllers.Features;

[Authorize]
[ApiController]
[Route("api/groups/{groupId}/features/[controller]")]
public class WordsChainsController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public WordsChainsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateWordsChainGame(int groupId, CreateWordsChainDto createWordsChainDto, CancellationToken cancellationToken)
    {
        var command = new CreateWordsChainGame.Command
        {
            GroupId = groupId,
            Topic = createWordsChainDto.Topic,
            UserId = GetRequiredAppUserId(),
        };
        
        var result = await _mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    [HttpPost("{gameId:int}")]
    public async Task<IActionResult> SendWordEntry(int groupId, int gameId, CreateWordsChainDto createWordsChainDto, CancellationToken cancellationToken)
    {
        var command = new CreateWordsChainGame.Command
        {
            GroupId = groupId,
            Topic = createWordsChainDto.Topic,
            UserId = GetRequiredAppUserId(),
        };

        var result = await _mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetWordsChainGames(int groupId, CancellationToken cancellationToken)
    {
        var query = new GetWordsChainGamesOverview.Query
        {
            GroupId = groupId,
            UserId = GetRequiredAppUserId(),
        };

        var result = await _mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpGet("{gameId:int}")]
    public async Task<IActionResult> GetWordsChainGameById(int groupId, int gameId, CancellationToken cancellationToken)
    {
        var query = new GetWordsChainGameById.Query
        {
            GroupId = groupId,
            Id = gameId,
            UserId = GetRequiredAppUserId(),
        };

        var result = await _mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpGet("{gameId:int}/entries")]
    public async Task<IActionResult> GetWordsChainGameEntries(int groupId, int gameId, [FromQuery] int pageSize = int.MaxValue, int pageNumber = 1,  CancellationToken cancellationToken = default)
    {
        var query = new GetWordsChainGameEntries.Query
        {
            GroupId = groupId,
            GameId = gameId,
            UserId = GetRequiredAppUserId(),
        };

        var result = await _mediator.Send(query, cancellationToken);

        return Ok(result);
    }
}

public class CreateWordsChainDto
{
    public string Topic { get; set; } = null!;
}