using MediatR;
using Microsoft.AspNetCore.Mvc;
using SyncLink.Application.Domain.Features.TextPlotGame;
using SyncLink.Application.UseCases.Features.TextPlotGame.Commands;
using SyncLink.Server.Controllers.Base;

namespace SyncLink.Server.Controllers.Features;

[ApiController]
[Route("[controller]")]
public class TextPlotGameController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public TextPlotGameController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("start")]
    public async Task<ActionResult<TextPlotGame>> StartGame([FromBody] StartGame.Command command)
    {
        var game = await _mediator.Send(command);
        return Ok(game);
    }

    [HttpPost("submitEntry")]
    public async Task<ActionResult<TextPlotEntry>> SubmitEntry([FromBody] SubmitEntry.Command command)
    {
        var entry = await _mediator.Send(command);
        return Ok(entry);
    }

    [HttpPost("voteEntry")]
    public async Task<ActionResult> VoteEntry([FromBody] VoteEntry.Command command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("endGame")]
    public async Task<ActionResult> EndGame([FromBody] EndGame.Command command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}