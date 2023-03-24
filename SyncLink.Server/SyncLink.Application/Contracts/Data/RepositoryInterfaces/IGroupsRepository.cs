using SyncLink.Application.Contracts.Data.Result;
using SyncLink.Application.Domain;

namespace SyncLink.Application.Contracts.Data.RepositoryInterfaces;

public interface IGroupsRepository : IEntityRepository<Group>
{
    Task<PaginatedRepositoryResultSet<Group>> SearchByNameAndDescriptionAsync(string[] terms, bool onlyMembership, CancellationToken cancellationToken);
}