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
    public async Task<ActionResult<TextPlotGame>> StartGame(StartGame.Command command, CancellationToken cancellationToken)
    {
        command.UserId = GetRequiredAppUserId();

        var game = await _mediator.Send(command, cancellationToken);

        return Ok(game);
    }

    [HttpPost("submitEntry")]
    public async Task<ActionResult<TextPlotEntry>> SubmitEntry([FromBody] SubmitEntry.Command command, CancellationToken cancellationToken)
    {
        command.UserId = GetRequiredAppUserId();
        var entry = await _mediator.Send(command, cancellationToken);

        return Ok(entry);
    }

    [HttpPost("voteEntry")]
    public async Task<ActionResult> VoteEntry([FromBody] VoteEntry.Command command, CancellationToken cancellationToken)
    {
        command.UserId = GetRequiredAppUserId();
        var result = await _mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    [HttpPost("endGame")]
    public async Task<ActionResult> EndGame([FromBody] EndGame.Command command, CancellationToken cancellationToken)
    {
        command.UserId = GetRequiredAppUserId();
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }
}