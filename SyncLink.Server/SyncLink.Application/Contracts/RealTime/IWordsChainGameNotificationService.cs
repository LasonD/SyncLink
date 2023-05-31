using SyncLink.Application.Dtos.WordsChainGame;

namespace SyncLink.Application.Contracts.RealTime;

public interface IWordsChainGameNotificationService
{
    Task NotifyNewEntryAsync(int groupId, WordsChainGameEntryDto entry, CancellationToken cancellationToken);

    Task NotifyNewWordsChainGameCreatedAsync(int groupId, WordsChainGameOverviewDto game, CancellationToken cancellationToken);
}