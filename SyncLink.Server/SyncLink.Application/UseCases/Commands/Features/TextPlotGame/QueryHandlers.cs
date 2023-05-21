using MediatR;
using Microsoft.EntityFrameworkCore;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;

namespace SyncLink.Application.UseCases.Commands.Features.TextPlotGame;

public class GetGameStateQueryHandler : IRequestHandler<GetGameStateQuery, Domain.Features.TextPlotGame>
{
    private readonly IAppDbContext _context;

    public GetGameStateQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Domain.Features.TextPlotGame> Handle(GetGameStateQuery request, CancellationToken cancellationToken)
    {
        var game = await _context.TextPlotGames
            .Include(g => g.Entries)
            .ThenInclude(e => e.Votes)
            .FirstOrDefaultAsync(g => g.Id == request.GameId, cancellationToken);

        return game;
    }
}

public class GetGroupGamesQueryHandler : IRequestHandler<GetGroupGamesQuery, IList<Domain.Features.TextPlotGame>>
{
    private readonly IAppDbContext _context;

    public GetGroupGamesQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<IList<Domain.Features.TextPlotGame>> Handle(GetGroupGamesQuery request, CancellationToken cancellationToken)
    {
        var games = await _context.TextPlotGames
            .Where(g => g.GroupId == request.GroupId)
            .ToListAsync(cancellationToken);

        return games;
    }
}