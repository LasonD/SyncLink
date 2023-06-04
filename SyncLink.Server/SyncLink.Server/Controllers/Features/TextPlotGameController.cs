using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyncLink.Application.Contracts.Data.Result.Pagination;
using SyncLink.Application.Domain.Features.TextPlotGame;
using SyncLink.Application.UseCases.Features.TextPlotGame.Commands;
using SyncLink.Application.UseCases.Features.TextPlotGame.Queries;
using SyncLink.Server.Controllers.Base;
using SyncLink.Server.Dtos.TextPlotGame;

namespace SyncLink.Server.Controllers.Features;

[Authorize]
[ApiController]
[Route("api/features/[controller]")]
public class TextPlotGamesController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public TextPlotGamesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IPaginatedResult<TextPlotGame>>> GetGroupGames([FromQuery] int groupId, CancellationToken cancellationToken)
    {
        var query = new GetGroupTextPlotGames.Query
        {
            GroupId = groupId,
            UserId = GetRequiredAppUserId()
        };

        var result = await _mediator.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpPost("start")]
    public async Task<ActionResult<TextPlotGame>> StartGame([FromBody] StartGameDto startGameDto, CancellationToken cancellationToken)
    {
        var command = new StartGame.Command
        {
            GroupId = startGameDto.GroupId,
            Topic = startGameDto.Topic,
            UserId = GetRequiredAppUserId()
        };

        var game = await _mediator.Send(command, cancellationToken);

        return Ok(game);
    }

    [HttpPost("submitEntry")]
    public async Task<ActionResult<TextPlotEntry>> SubmitEntry([FromBody] SubmitEntryDto submitEntryDto, CancellationToken cancellationToken)
    {
        var command = new   SubmitEntry.Command
        {
            GameId = submitEntryDto.GameId,
            Text = submitEntryDto.Text,
            UserId = GetRequiredAppUserId()
        };

        var entry = await _mediator.Send(command, cancellationToken);

        return Ok(entry);
    }

    [HttpPost("voteEntry")]
    public async Task<ActionResult> VoteEntry([FromBody] VoteEntryDto voteEntryDto, CancellationToken cancellationToken)
    {
        var command = new VoteEntry.Command
        {
            EntryId = voteEntryDto.EntryId,
            GameId = voteEntryDto.GameId,
            UserId = GetRequiredAppUserId()
        };

        var result = await _mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    [HttpPost("endGame")]
    public async Task<ActionResult> EndGame([FromBody] EndGameDto endGameDto, CancellationToken cancellationToken)
    {
        var command = new EndGame.Command
        {
            GameId = endGameDto.GameId,
            GroupId = endGameDto.GroupId,
            UserId = GetRequiredAppUserId()
        };

        var result = await _mediator.Send(command, cancellationToken);

        return Ok(result);
    }
}