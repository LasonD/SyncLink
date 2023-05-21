using MediatR;

namespace SyncLink.Application.UseCases.Commands.Features.TextPlotGame;

public class GetGameStateQuery : IRequest<Domain.Features.TextPlotGame>
{
    public int GameId { get; set; }
}

public class GetGroupGamesQuery : IRequest<IList<Domain.Features.TextPlotGame>>
{
    public int GroupId { get; set; }
}