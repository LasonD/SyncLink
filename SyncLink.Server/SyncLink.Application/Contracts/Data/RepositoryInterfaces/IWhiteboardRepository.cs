using SyncLink.Application.Contracts.Data.Result;
using SyncLink.Application.Domain.Features;

namespace SyncLink.Application.Contracts.Data.RepositoryInterfaces;

public interface IWhiteboardRepository : IEntityRepository<Whiteboard>
{
    Task<PaginatedRepositoryResultSet<Whiteboard>> GetGroupWhiteboardsAsync(int groupId, OrderedPaginationQuery<Whiteboard> query, CancellationToken cancellationToken);
}