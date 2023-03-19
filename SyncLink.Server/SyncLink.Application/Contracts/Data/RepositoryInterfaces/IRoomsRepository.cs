using SyncLink.Application.Contracts.Data.Result;
using SyncLink.Application.Domain;

namespace SyncLink.Application.Contracts.Data.RepositoryInterfaces;

public interface IRoomsRepository : IEntityRepository<Room>
{
    Task<RepositoryEntityResult<Room>> GetRoomForUserAsync(int userId, int roomId, CancellationToken cancellationToken);

    Task<RepositoryEntityResult<Room>> GetPrivateRoomAsync(int firstUserId, int secondUserId, CancellationToken cancellationToken);
}