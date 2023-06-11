using SyncLink.Application.Contracts.Data;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Contracts.Data.Result;
using SyncLink.Application.Domain.Groups.Rooms;
using SyncLink.Infrastructure.Data.Context;

namespace SyncLink.Infrastructure.Data.Repositories;

public class MessagesRepository : GenericEntityRepository<Message>, IMessagesRepository
{
    public MessagesRepository(SyncLinkDbContext dbContext) : base(dbContext)
    {
    }

    public Task<PaginatedRepositoryResultSet<Message>> GetRoomMessagesAsync(int groupId, int roomId, OrderedPaginationQuery<Message> query, CancellationToken cancellationToken)
    {
        query.FilteringExpressions.Add(m => m.RoomId == roomId && m.Room.GroupId == groupId);
        query.OrderingExpressions.Add(new OrderingCriteria<Message>(m => m.CreationDate, IsAscending: false));

        return GetBySpecificationAsync(query, cancellationToken);
    }
}