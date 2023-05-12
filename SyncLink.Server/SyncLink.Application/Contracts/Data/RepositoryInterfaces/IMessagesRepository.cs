using SyncLink.Application.Contracts.Data.Result;
using SyncLink.Application.Domain;

namespace SyncLink.Application.Contracts.Data.RepositoryInterfaces;

public interface IMessagesRepository : IEntityRepository<Message>
{
    Task<PaginatedRepositoryResultSet<Message>> GetRoomMessagesAsync(int groupId, int roomId, OrderedPaginationQuery<Message> query, CancellationToken cancellationToken);
}