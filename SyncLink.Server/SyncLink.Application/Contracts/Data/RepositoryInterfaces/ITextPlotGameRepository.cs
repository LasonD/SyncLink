using SyncLink.Application.Contracts.Data.Result;
using SyncLink.Application.Domain.Features.TextPlotGame;

namespace SyncLink.Application.Contracts.Data.RepositoryInterfaces;

public interface ITextPlotGameRepository : IEntityRepository<TextPlotGame>
{
    Task<PaginatedRepositoryResultSet<TextPlotEntry>> GetTextPlotGameEntriesAsync(int groupId, int gameId, OrderedPaginationQuery<TextPlotEntry> query, CancellationToken cancellationToken);

    Task<RepositoryEntityResult<TextPlotEntry>> GetPendingEntryWithMostVotesAsync(int groupId, int gameId, CancellationToken cancellationToken);
    Task<RepositoryEntityResult<TextPlotGame>> GetTextPlotGameCompleteAsync(int groupId, int gameId, CancellationToken cancellationToken);

    Task<PaginatedRepositoryResultSet<TextPlotEntry>> GetPendingEntriesAsync(int groupId, int gameId, CancellationToken cancellationToken);
}