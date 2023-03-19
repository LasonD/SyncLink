using SyncLink.Application.Contracts.Data.Result;
using SyncLink.Application.Domain;

namespace SyncLink.Application.Contracts.Data.RepositoryInterfaces;

public interface IUserRepository : IEntityRepository<User>
{
    Task<bool> UserHasGroupWithNameAsync(int userId, string groupName, CancellationToken cancellationToken);

    Task<PaginatedRepositoryResultSet<User>> GetUsersFromGroupAsync(int groupId, IEnumerable<int> userIds, CancellationToken cancellationToken);
}