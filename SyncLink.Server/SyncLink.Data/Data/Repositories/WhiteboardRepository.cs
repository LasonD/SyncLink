using SyncLink.Application.Contracts.Data.Result;
using SyncLink.Application.Contracts.Data;
using SyncLink.Application.Domain.Features;
using SyncLink.Infrastructure.Data.Context;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;

namespace SyncLink.Infrastructure.Data.Repositories;

public class WhiteboardRepository : GenericEntityRepository<Whiteboard>, IWhiteboardRepository
{
    public WhiteboardRepository(SyncLinkDbContext dbContext) : base(dbContext)
    {
    }

    public Task<PaginatedRepositoryResultSet<Whiteboard>> GetGroupWhiteboardsAsync(int groupId, OrderedPaginationQuery<Whiteboard> query, CancellationToken cancellationToken)
    {
        query.FilteringExpressions.Add(x => x.GroupId == groupId);

        return GetBySpecificationAsync(query, cancellationToken);
    }
}