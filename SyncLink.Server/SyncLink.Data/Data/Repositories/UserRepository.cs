using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Domain;
using SyncLink.Infrastructure.Data.Context;

namespace SyncLink.Infrastructure.Data.Repositories;

public class UserRepository : GenericEntityRepository<User>, IUserRepository
{
    public UserRepository(SyncLinkDbContext dbContext) : base(dbContext)
    {
    }
}