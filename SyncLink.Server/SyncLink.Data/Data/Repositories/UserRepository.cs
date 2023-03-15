using Microsoft.EntityFrameworkCore;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Domain;
using SyncLink.Infrastructure.Data.Context;

namespace SyncLink.Infrastructure.Data.Repositories;

public class UserRepository : GenericEntityRepository<User>, IUserRepository
{
    public UserRepository(SyncLinkDbContext dbContext) : base(dbContext)
    {
    }

    public Task<bool> UserHasGroupWithNameAsync(int userId, string groupName, CancellationToken cancellationToken)
    {
        return DbContext.UsersToGroups.AnyAsync(ug =>
                ug.UserId == userId &&
                ug.Group.Name == groupName &&
                ug.IsCreator, cancellationToken
        );
    }
}