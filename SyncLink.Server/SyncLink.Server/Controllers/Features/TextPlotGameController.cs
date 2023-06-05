using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyncLink.Application.UseCases.Features.TextPlotGame.Commands;
using SyncLink.Application.UseCases.Features.TextPlotGame.Queries;
using SyncLink.Server.Controllers.Base;
using SyncLink.Server.Dtos.Pagination;
using SyncLink.Server.Dtos.TextPlotGame;

namespace SyncLink.Server.Controllers.Features;

[Authorize]
[ApiController]
[Route("api/{groupId}features/[controller]")]
public class TextPlotGamesController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public TextPlotGamesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetGroupGames(int groupId, [FromQuery] PageQueryParams page, CancellationToken cancellationToken)
    {
        var query = new GetGroupTextPlotGames.Query
        {
            GroupId = groupId,
            UserId = GetRequiredAppUserId(),
            PageNumber = page.PageNumber,
            PageSize = page.PageSize
        };

        var result = await _mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpGet("{gameId}/entries")]
    public async Task<IActionResult> GetTextPlotGameEntries(int groupId, int gameId, [FromQuery] PageQueryParams page, CancellationToken cancellationToken)
    {
        var query = new GetTextPlotGameEntries.Query
        {
            GroupId = groupId,
            GameId = gameId,
            UserId = GetRequiredAppUserId(),
            PageNumber = page.PageNumber,
            PageSize = page.PageSize
        };

        var result = await _mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpPost("{gameId}/entries/{entryId}")]
    public async Task<IActionResult> VoteEntry(int groupId, int gameId, int entryId, [FromBody] VoteEntryDto voteEntryDto, CancellationToken cancellationToken)
    {
        var command = new VoteEntry.Command
        {
            EntryId = entryId,
            GameId = gameId,
            GroupId = groupId,
            UserId = GetRequiredAppUserId(),
            Comment = voteEntryDto.Comment
        };

        var result = await _mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    [HttpPost("{gameId}/entries")]
    public async Task<IActionResult> SubmitEntry(int groupId, [FromBody] SubmitEntryDto submitEntryDto, CancellationToken cancellationToken)
    {
        var command = new SubmitEntry.Command
        {
            GameId = submitEntryDto.GameId,
            Text = submitEntryDto.Text,
            GroupId = groupId,
            UserId = GetRequiredAppUserId()
        };

        var entry = await _mediator.Send(command, cancellationToken);

        return Ok(entry);
    }

    [HttpPost]
    public async Task<IActionResult> StartGame(int groupId, [FromBody] StartGameDto startGameDto, CancellationToken cancellationToken)
    {
        var command = new StartGame.Command
        {
            GroupId = groupId,
            Topic = startGameDto.Topic,
            UserId = GetRequiredAppUserId()
        };

        var game = await _mediator.Send(command, cancellationToken);

        return Ok(game);
    }

    // [HttpPost("endGame")]
    // public async Task<IActionResult> EndGame(int groupId, [FromBody] EndGameDto endGameDto, CancellationToken cancellationToken)
    // {
    //     var command = new EndGame.Command
    //     {
    //         GameId = endGameDto.GameId,
    //         GroupId = groupId,
    //         UserId = GetRequiredAppUserId()
    //     };
    //
    //     var result = await _mediator.Send(command, cancellationToken);
    //
    //     return Ok(result);
    // }
}