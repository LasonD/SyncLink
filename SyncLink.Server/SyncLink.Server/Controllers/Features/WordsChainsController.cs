using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyncLink.Application.UseCases.Features.TextChainGame.Commands;
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
}

public class CreateWordsChainDto
{
    public string Topic { get; set; } = null!;
}