using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Domain;
using SyncLink.Infrastructure.Data.Context;

namespace SyncLink.Infrastructure.Data.Repositories;

public class GroupsRepository : GenericEntityRepository<Group>, IGroupRepository
{
    public GroupsRepository(SyncLinkDbContext dbContext) : base(dbContext)
    {
    }
}