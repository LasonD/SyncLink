﻿using Microsoft.EntityFrameworkCore;
using SyncLink.Application.Contracts.Data;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Contracts.Data.Result;
using SyncLink.Application.Domain.Groups.Rooms;
using SyncLink.Infrastructure.Data.Context;

namespace SyncLink.Infrastructure.Data.Repositories;

public class RoomsRepository : GenericEntityRepository<Room>, IRoomsRepository
{
    public RoomsRepository(SyncLinkDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<RepositoryEntityResult<Room>> GetRoomForUserAsync(int groupId, int userId, int roomId, CancellationToken cancellationToken)
    {
        var room = await DbContext.Rooms
            .SingleOrDefaultAsync(r => r.Id == roomId && r.GroupId == groupId && r.RoomMembers.Any(m => m.UserId == userId), cancellationToken);

        if (room == null)
        {
            return RepositoryEntityResult<Room>.NotFound(new RepositoryError[] { $"Room with id {roomId} not found for user with id {userId}." });
        }

        return RepositoryEntityResult<Room>.Ok(room);
    }

    public Task<PaginatedRepositoryResultSet<Room>> GetRoomsForUserAsync(int groupId, int userId, OrderedPaginationQuery<Room> query, CancellationToken cancellationToken)
    {
        query.FilteringExpressions.Add(r => r.IsPrivate == false && r.GroupId == groupId && r.RoomMembers.Any(m => m.UserId == userId));

        return GetBySpecificationAsync(query, cancellationToken);
    }

    public async Task<RepositoryEntityResult<Room>> GetPrivateRoomAsync(int groupId, int firstUserId, int secondUserId, CancellationToken cancellationToken)
    {
        var room = await DbContext.Rooms.FirstOrDefaultAsync(r =>
                r.IsPrivate &&
                r.GroupId == groupId &&
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