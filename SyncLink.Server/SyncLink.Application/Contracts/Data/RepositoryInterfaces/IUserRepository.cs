using SyncLink.Application.Contracts.Data.Result;
using SyncLink.Application.Domain;
using SyncLink.Application.Domain.Associations;

namespace SyncLink.Application.Contracts.Data.RepositoryInterfaces;

public interface IUserRepository : IEntityRepository<User>
{
    Task<bool> UserHasGroupWithNameAsync(int userId, string groupName, CancellationToken cancellationToken);
    Task<bool> IsUserInGroupAsync(int userId, int groupId, CancellationToken cancellationToken);

    Task<PaginatedRepositoryResultSet<User>> GetUsersFromGroupAsync(int groupId, IEnumerable<int> userIds, CancellationToken cancellationToken);

    Task<PaginatedRepositoryResultSet<UserGroup>> GetGroupMembersAsync(int groupId, OrderedPaginationQuery<UserGroup> query, CancellationToken cancellationToken);
}