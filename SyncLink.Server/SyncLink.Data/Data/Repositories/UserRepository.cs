﻿using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SyncLink.Application.Contracts.Data;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Contracts.Data.Result;
using SyncLink.Application.Domain;
using SyncLink.Application.Domain.Groups;
using SyncLink.Application.Domain.Groups.Rooms;
using SyncLink.Infrastructure.Data.Context;
using SyncLink.Infrastructure.Data.Helpers;

namespace SyncLink.Infrastructure.Data.Repositories;

public class UserRepository : GenericEntityRepository<User>, IUserRepository
{
    public UserRepository(SyncLinkDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<PaginatedRepositoryResultSet<User>> GetUsersFromGroupAsync(int groupId, IEnumerable<int> userIds, CancellationToken cancellationToken)
    {
        var specification = new OrderedPaginationQuery<User>
        {
            FilteringExpressions = new List<Expression<Func<User, bool>>>()
            {
                u => userIds.Contains(u.Id),
                u => u.UserGroups.Any(ug => ug.GroupId == groupId),
            },
            Page = 1,
            PageSize = int.MaxValue
        };

        var query = DbContext.ApplicationUsers;
        var (specifiedQuery, totalCount) = await ApplyQuerySpecificationAsync(query, specification, cancellationToken);
        var users = await specifiedQuery.ToListAsync(cancellationToken);

        return users.ToPaginatedOkResult(specification.Page, specification.PageSize, totalCount);
    }

    public async Task<RepositoryEntityResult<User>> GetUserFromGroupAsync(int groupId, int userId, CancellationToken cancellationToken)
    {
        var user = await DbContext.ApplicationUsers
            .Where(u => u.Id == userId && u.UserGroups.Any(ug => ug.GroupId == groupId))
            .FirstOrDefaultAsync(cancellationToken);

        return RepositoryEntityResult<User>.FromEntity(user);
    }

    public async Task<PaginatedRepositoryResultSet<UserGroup>> GetGroupMembersAsync(int groupId, OrderedPaginationQuery<UserGroup> query, CancellationToken cancellationToken)
    {
        query.FilteringExpressions.Add(utg => utg.GroupId == groupId);
        query.IncludeExpressions.Add(utg => utg.User);

        return await GetBySpecificationAsync(query, cancellationToken);
    }

    public async Task<PaginatedRepositoryResultSet<UserRoom>> GetRoomMembersAsync(int groupId, int roomId, OrderedPaginationQuery<UserRoom> query, CancellationToken cancellationToken)
    {
        query.FilteringExpressions.Add(utr => utr.RoomId == roomId && utr.Room.GroupId == groupId);
        query.IncludeExpressions.Add(utg => utg.User);

        return await GetBySpecificationAsync(query, cancellationToken);
    }

    public Task<bool> IsUserAdminOfGroupAsync(int userId, int groupId, CancellationToken cancellationToken)
    {
        return DbContext.UsersToGroups.AnyAsync(ug =>
                ug.UserId == userId &&
                ug.GroupId == groupId &&
                ug.IsAdmin, cancellationToken
        );
    }

    public Task<bool> UserHasGroupWithNameAsync(int userId, string groupName, CancellationToken cancellationToken)
    {
        return DbContext.UsersToGroups.AnyAsync(ug =>
                ug.UserId == userId &&
                ug.Group.Name == groupName &&
                ug.IsCreator, cancellationToken
        );
    }

    public Task<bool> IsUserInGroupAsync(int userId, int groupId, CancellationToken cancellationToken)
    {
        return DbContext.UsersToGroups.AnyAsync(utg => utg.UserId == userId && utg.GroupId == groupId, cancellationToken);
    }
}