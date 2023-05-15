using SyncLink.Application.Contracts.Data.Result;
using SyncLink.Application.Domain;

namespace SyncLink.Application.Contracts.Data.RepositoryInterfaces;

public interface IRoomsRepository : IEntityRepository<Room>
{
    Task<RepositoryEntityResult<Room>> GetRoomForUserAsync(int groupId, int userId, int roomId, CancellationToken cancellationToken);

    Task<PaginatedRepositoryResultSet<Room>> GetRoomsForUserAsync(int groupId, int userId, OrderedPaginationQuery<Room> query, CancellationToken cancellationToken);

    Task<RepositoryEntityResult<Room>> GetPrivateRoomAsync(int groupId, int firstUserId, int secondUserId, CancellationToken cancellationToken);
}