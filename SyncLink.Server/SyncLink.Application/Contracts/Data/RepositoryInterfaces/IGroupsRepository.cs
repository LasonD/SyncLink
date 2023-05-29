using SyncLink.Application.Contracts.Data.Enums;
using SyncLink.Application.Contracts.Data.Result;
using SyncLink.Application.Domain;

namespace SyncLink.Application.Contracts.Data.RepositoryInterfaces;

public interface IGroupsRepository : IEntityRepository<Group>
{
    Task<PaginatedRepositoryResultSet<Group>> SearchByNameAndDescriptionAsync(int userId, string[] terms, GroupSearchMode searchMode, CancellationToken cancellationToken);

    Task<int> GetGroupMembersCountAsync(int groupId, CancellationToken cancellationToken);
}