using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Domain;
using SyncLink.Infrastructure.Data.Context;

namespace SyncLink.Infrastructure.Data.Repositories;

public class RoomsRepository : GenericEntityRepository<Room>, IRoomsRepository
{
    public RoomsRepository(SyncLinkDbContext dbContext) : base(dbContext)
    {
    }
}