using Microsoft.EntityFrameworkCore;
using SyncLink.Application.Contracts.Data;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Contracts.Data.Result;
using SyncLink.Application.Domain.Features;
using SyncLink.Infrastructure.Data.Context;

namespace SyncLink.Infrastructure.Data.Repositories;

public class WordsChainGamesRepository : GenericEntityRepository<WordsChainGame>, IWordsChainGamesRepository 
{
    public WordsChainGamesRepository(SyncLinkDbContext dbContext) : base(dbContext)
    {
    }

    public Task<PaginatedRepositoryResultSet<WordsChainGame>> GetGroupWordsChainGamesAsync(int groupId, OrderedPaginationQuery<WordsChainGame> query, CancellationToken cancellationToken)
    {
        query.FilteringExpressions.Add(x => x.GroupId == groupId);

        return GetBySpecificationAsync(query, cancellationToken);
    }

    public Task<PaginatedRepositoryResultSet<WordsChainEntry>> GetWordsChainGameEntriesAsync(int groupId, int gameId, OrderedPaginationQuery<WordsChainEntry> query, CancellationToken cancellationToken)
    {
        query.IncludeExpressions.Add(m => m.Game);
        query.IncludeExpressions.Add(m => m.ParticipantId);
        query.FilteringExpressions.Add(m => m.GameId == gameId && m.Game.GroupId == groupId);
        query.OrderingExpressions.Add(new OrderingCriteria<WordsChainEntry>(m => m.CreationDate, IsAscending: false));

        return GetBySpecificationAsync(query, cancellationToken);
    }

    public Task<bool> CheckGameAlreadyHasWordAsync(int gameId, string word, CancellationToken cancellationToken)
    {
        var trimmedWord = word.Trim().ToLowerInvariant();
        return DbContext.Set<WordsChainEntry>().AnyAsync(m => m.GameId == gameId && m.Word == trimmedWord, cancellationToken);
    }

    public Task<bool> CheckUserIsParticipantAsync(int gameId, int userId, CancellationToken cancellationToken)
    {
        return DbContext.Set<UserToWordsChainGame>().AnyAsync(m => m.GameId == gameId && m.UserId == userId, cancellationToken);
    }
}