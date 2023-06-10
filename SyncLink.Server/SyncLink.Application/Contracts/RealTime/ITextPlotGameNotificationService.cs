using SyncLink.Application.Dtos.TextPlotGame;

namespace SyncLink.Application.Contracts.RealTime;

public interface ITextPlotGameNotificationService
{
    Task NotifyGameStartedAsync(int groupId, TextPlotGameDto game, CancellationToken cancellationToken);
    Task NotifyNewEntryAsync(int groupId, TextPlotEntryDto entry, CancellationToken cancellationToken);
    Task NotifyEntryCommittedAsync(int groupId, TextPlotEntryDto entry, CancellationToken cancellationToken);
    Task NotifyEntryNotCommittedAsync(int groupId, int gameId, CancellationToken cancellationToken);
    Task NotifyVoteReceivedAsync(int groupId, TextPlotVoteDto vote, CancellationToken cancellationToken);
    Task NotifyVoteRevokedAsync(int groupId, int gameId, int voteId, CancellationToken cancellationToken);
    Task NotifyGameEndedAsync(int groupId, TextPlotGameDto game, CancellationToken cancellationToken);
    Task NotifyEntriesDiscardedAsync(int groupId, int[] discardedEntryIds, CancellationToken cancellationToken);
}