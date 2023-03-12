using SyncLink.Application.Contracts.Data.Result;
using SyncLink.Application.Domain;

namespace SyncLink.Application.Contracts.Data.RepositoryInterfaces;

public interface IGroupRepository : IEntityRepository<Group>
{
    Task<RepositoryEntityResult<Group>> CreateGroupForUserAsync(int userId, Group group, CancellationToken cancellationToken);
}