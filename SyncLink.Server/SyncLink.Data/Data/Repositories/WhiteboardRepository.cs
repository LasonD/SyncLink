using SyncLink.Application.Domain.Features;
using SyncLink.Infrastructure.Data.Context;

namespace SyncLink.Infrastructure.Data.Repositories;

public class WhiteboardRepository : GenericEntityRepository<Whiteboard>
{
    public WhiteboardRepository(SyncLinkDbContext dbContext) : base(dbContext)
    {
    }
}