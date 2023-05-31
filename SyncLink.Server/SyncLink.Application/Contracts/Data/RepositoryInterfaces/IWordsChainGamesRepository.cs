using SyncLink.Application.Contracts.Data.Result;
using SyncLink.Application.Domain.Features;

namespace SyncLink.Application.Contracts.Data.RepositoryInterfaces;

public interface IWordsChainGamesRepository : IEntityRepository<WordsChainGame>
{
    Task<PaginatedRepositoryResultSet<WordsChainGame>> GetGroupWordsChainGamesAsync(int groupId, OrderedPaginationQuery<WordsChainGame> query, CancellationToken cancellationToken);
    Task<PaginatedRepositoryResultSet<WordsChainEntry>> GetWordsChainGameEntriesAsync(int groupId, int gameId, OrderedPaginationQuery<WordsChainEntry> query, CancellationToken cancellationToken);
    Task<bool> CheckGameAlreadyHasWordAsync(int gameId, string word, CancellationToken cancellationToken);
    Task<bool> CheckUserIsParticipantAsync(int gameId, int userId, CancellationToken cancellationToken);
}