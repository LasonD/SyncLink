using MediatR;
using SyncLink.Application.Domain.Features;

namespace SyncLink.Application.UseCases.Commands.Features.TextPlotGame;

public class StartGameCommand : IRequest<Domain.Features.TextPlotGame>
{
    public int GroupId { get; set; }
    public int StarterId { get; set; }
}

public class SubmitEntryCommand : IRequest<TextPlotEntry>
{
    public int GameId { get; set; }
    public int UserId { get; set; }
    public string Text { get; set; } = null!;
}

public class VoteEntryCommand : IRequest
{
    public int EntryId { get; set; }
    public int UserId { get; set; }
}

public class EndGameCommand : IRequest
{
    public int GameId { get; set; }
}
