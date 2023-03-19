using Microsoft.EntityFrameworkCore;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Contracts.Data.Result;
using SyncLink.Application.Domain;
using SyncLink.Infrastructure.Data.Context;

namespace SyncLink.Infrastructure.Data.Repositories;

public class RoomsRepository : GenericEntityRepository<Room>, IRoomsRepository
{
    public RoomsRepository(SyncLinkDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<RepositoryEntityResult<Room>> GetRoomForUserAsync(int userId, int roomId, CancellationToken cancellationToken)
    {
        var room = await DbContext.Rooms
            .SingleOrDefaultAsync(r => r.Id == roomId && r.RoomMembers.Any(m => m.UserId == userId), cancellationToken);

        if (room == null)
        {
            return RepositoryEntityResult<Room>.NotFound(new RepositoryError[] { $"Room with id {roomId} not found for user with id {userId}." });
        }

        return RepositoryEntityResult<Room>.Ok(room);
    }

    public async Task<RepositoryEntityResult<Room>> GetPrivateRoomAsync(int firstUserId, int secondUserId, CancellationToken cancellationToken)
    {
        var room = await DbContext.Rooms.SingleOrDefaultAsync(r =>
                r.IsPrivate &&
                r.RoomMembers.Any(um => um.UserId == firstUserId) &&
                r.RoomMembers.Any(um => um.UserId == secondUserId),
            cancellationToken
        );

        if (room == null)
        {
            return RepositoryEntityResult<Room>.NotFound(new RepositoryError[] { $"No private room for users {firstUserId} and {secondUserId} exists." });
        }

        return RepositoryEntityResult<Room>.Ok(room);
    }
}