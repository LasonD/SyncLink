using Microsoft.EntityFrameworkCore;
using SyncLink.Application.Contracts.Data;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Contracts.Data.Result;
using SyncLink.Application.Domain.Features.TextPlotGame;
using SyncLink.Infrastructure.Data.Context;

namespace SyncLink.Infrastructure.Data.Repositories;

public class TextPlotGameRepository : GenericEntityRepository<TextPlotGame>, ITextPlotGameRepository
{
    public TextPlotGameRepository(SyncLinkDbContext dbContext) : base(dbContext)
    {
    }

    public Task<PaginatedRepositoryResultSet<TextPlotEntry>> GetTextPlotGameEntriesAsync(int groupId, int gameId, OrderedPaginationQuery<TextPlotEntry> query, CancellationToken cancellationToken)
    {
        query.OrderingExpressions.Add(new OrderingCriteria<TextPlotEntry>(x => x.CreationDate, false));
        query.FilteringExpressions.Add(e => e.GameId == gameId && e.Game.GroupId == groupId);

        return GetBySpecificationAsync(query, cancellationToken);
    }

    public async Task<RepositoryEntityResult<TextPlotEntry>> GetPendingEntryWithMostVotesAsync(int groupId, int gameId, CancellationToken cancellationToken)
    {
        var entry = await DbContext.TextPlotEntries
            .Where(e => e.GameId == gameId && e.Game.GroupId == groupId && !e.IsCommitted)
            .OrderByDescending(e => e.Votes.Count)
            .FirstOrDefaultAsync(cancellationToken);

        return RepositoryEntityResult<TextPlotEntry>.FromEntity(entry);
    }
}