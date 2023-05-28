using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyncLink.Application.Domain.Features.TextPlotGame;
using SyncLink.Application.UseCases.Features.TextPlotGame.Commands;
using SyncLink.Server.Controllers.Base;

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

    [HttpPost("start")]
    public async Task<ActionResult<TextPlotGame>> StartGame([FromBody] StartGame.StartGameCommand startGameCommand, CancellationToken cancellationToken)
    {
        startGameCommand.UserId = GetRequiredAppUserId();

        var game = await _mediator.Send(startGameCommand, cancellationToken);

        return Ok(game);
    }

    [HttpPost("submitEntry")]
    public async Task<ActionResult<TextPlotEntry>> SubmitEntry([FromBody] SubmitEntry.SubmitEntryCommand submitEntryCommand, CancellationToken cancellationToken)
    {
        submitEntryCommand.UserId = GetRequiredAppUserId();
        var entry = await _mediator.Send(submitEntryCommand, cancellationToken);

        return Ok(entry);
    }

    [HttpPost("voteEntry")]
    public async Task<ActionResult> VoteEntry([FromBody] VoteEntry.VoteCommand voteCommand, CancellationToken cancellationToken)
    {
        voteCommand.UserId = GetRequiredAppUserId();
        var result = await _mediator.Send(voteCommand, cancellationToken);

        return Ok(result);
    }

    [HttpPost("endGame")]
    public async Task<ActionResult> EndGame([FromBody] EndGame.EndGameCommand endGameCommand, CancellationToken cancellationToken)
    {
        endGameCommand.UserId = GetRequiredAppUserId();
        var result = await _mediator.Send(endGameCommand, cancellationToken);
        return Ok(result);
    }
}