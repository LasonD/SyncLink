using Microsoft.AspNetCore.SignalR;
using SyncLink.Application.Contracts.RealTime;
using SyncLink.Application.Dtos.TextPlotGame;
using SyncLink.Server.Helpers;
using SyncLink.Server.Hubs;

namespace SyncLink.Server.SignalR.TextPlotGame;

internal class TextPlotGameNotificationService : ITextPlotGameNotificationService
{
    private readonly IHubContext<SyncLinkHub, ISyncLinkHub> _hubContext;

    public TextPlotGameNotificationService(IHubContext<SyncLinkHub, ISyncLinkHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public Task NotifyGameStartedAsync(int groupId, TextPlotGameDto game, CancellationToken cancellationToken)
    {
        var groupName = HubHelper.GetGroupNameForGroupId(groupId);
        return _hubContext.Clients.Group(groupName).GameStarted(game);
    }

    public Task NotifyNewEntryAsync(int groupId, TextPlotEntryDto entry, CancellationToken cancellationToken)
    {
        var groupName = HubHelper.GetGroupNameForGroupId(groupId);
        return _hubContext.Clients.Group(groupName).NewEntry(entry);
    }

    public Task NotifyEntryCommittedAsync(int groupId, TextPlotEntryDto entry, CancellationToken cancellationToken)
    {
        var groupName = HubHelper.GetGroupNameForGroupId(groupId);
        return _hubContext.Clients.Group(groupName).EntryCommitted(entry);
    }

    public Task NotifyEntryNotCommittedAsync(int groupId, int gameId, CancellationToken cancellationToken)
    {
        var groupName = HubHelper.GetGroupNameForGroupId(groupId);
        return _hubContext.Clients.Group(groupName).EntryNotCommitted(gameId);
    }

    public Task NotifyVoteReceivedAsync(int groupId, TextPlotVoteDto vote, CancellationToken cancellationToken)
    {
        var groupName = HubHelper.GetGroupNameForGroupId(groupId);
        return _hubContext.Clients.Group(groupName).VoteReceived(vote);
    }

    public Task NotifyGameEndedAsync(int groupId, TextPlotGameDto game, CancellationToken cancellationToken)
    {
        var groupName = HubHelper.GetGroupNameForGroupId(groupId);
        return _hubContext.Clients.Group(groupName).GameEnded(game);
    }

    public Task NotifyEntriesDiscardedAsync(int groupId, int[] discardedEntryIds, CancellationToken cancellationToken)
    {
        var groupName = HubHelper.GetGroupNameForGroupId(groupId);
        return _hubContext.Clients.Group(groupName).EntriesDiscarded(discardedEntryIds);
    }
}