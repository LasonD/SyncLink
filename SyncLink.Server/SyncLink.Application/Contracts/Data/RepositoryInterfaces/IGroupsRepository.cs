using SyncLink.Application.Contracts.Data.Result;
using SyncLink.Application.Domain;

namespace SyncLink.Application.Contracts.Data.RepositoryInterfaces;

public interface IGroupsRepository : IEntityRepository<Group>
{
    Task<RepositoryEntityResult<Group>> GetGroupForUserAsync(int userId, int groupId, CancellationToken cancellationToken);
}