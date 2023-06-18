using Microsoft.EntityFrameworkCore;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces.Factories;
using SyncLink.Infrastructure.Data.Context;

namespace SyncLink.Infrastructure.Data.Repositories.Factories;

public class TextPlotGameRepositoryFactory : ITextPlotGameRepositoryFactory
{
    private readonly IDbContextFactory<SyncLinkDbContext> _dbContextFactory;

    public TextPlotGameRepositoryFactory(IDbContextFactory<SyncLinkDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public ITextPlotGameRepository Create()
    {
        var dbContext = _dbContextFactory.CreateDbContext();
        return new TextPlotGameRepository(dbContext);
    }
}