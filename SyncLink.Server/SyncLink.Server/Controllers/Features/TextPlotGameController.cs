using MediatR;
using Microsoft.AspNetCore.Mvc;
using SyncLink.Application.Domain.Features;
using SyncLink.Application.UseCases.Commands.Features.TextPlotGame;

namespace SyncLink.Server.Controllers.Features;

[ApiController]
[Route("[controller]")]
public class TextPlotGameController : ControllerBase
{
    private readonly IMediator _mediator;

    public TextPlotGameController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("start")]
    public async Task<ActionResult<TextPlotGame>> StartGame([FromBody] StartGameCommand command)
    {
        var game = await _mediator.Send(command);
        return Ok(game);
    }

    [HttpPost("submitEntry")]
    public async Task<ActionResult<TextPlotEntry>> SubmitEntry([FromBody] SubmitEntryCommand command)
    {
        var entry = await _mediator.Send(command);
        return Ok(entry);
    }

    [HttpPost("voteEntry")]
    public async Task<ActionResult> VoteEntry([FromBody] VoteEntryCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [HttpPost("endGame")]
    public async Task<ActionResult> EndGame([FromBody] EndGameCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
}