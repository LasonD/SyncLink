using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Contracts.Data.Result;
using SyncLink.Application.Domain;
using SyncLink.Infrastructure.Data.Context;

namespace SyncLink.Infrastructure.Data.Repositories;

public class GroupsRepository : GenericEntityRepository<Group>, IGroupsRepository
{
    public GroupsRepository(SyncLinkDbContext dbContext) : base(dbContext)
    {
    }

    // public Task<RepositoryEntityResult<Group>> GetGroupForUserAsync(int userId, int groupId, CancellationToken cancellationToken)
    // {
    //     var result = GetBySpecificationAsync<>
    // }
}