using SyncLink.Application.Domain.Features.TextPlotGame;

namespace SyncLink.Application.Contracts.RealTime;

public interface ITextPlotGameNotificationService
{
    Task NotifyGameStartedAsync(int groupId, TextPlotGame game, CancellationToken cancellationToken);
    Task NotifyNewEntryAsync(int groupId, TextPlotEntry entry, CancellationToken cancellationToken);
    Task NotifyVoteReceivedAsync(int groupId, TextPlotVote vote, CancellationToken cancellationToken);
    Task NotifyGameEndedAsync(int groupId, TextPlotGame game, CancellationToken cancellationToken);
}