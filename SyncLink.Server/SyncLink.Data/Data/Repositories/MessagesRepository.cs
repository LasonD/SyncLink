using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Domain;
using SyncLink.Infrastructure.Data.Context;

namespace SyncLink.Infrastructure.Data.Repositories;

public class MessagesRepository : GenericEntityRepository<Message>, IMessagesRepository
{
    public MessagesRepository(SyncLinkDbContext dbContext) : base(dbContext)
    {
    }
}