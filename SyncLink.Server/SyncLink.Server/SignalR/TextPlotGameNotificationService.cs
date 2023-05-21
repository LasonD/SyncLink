using Microsoft.AspNetCore.SignalR;
using SyncLink.Application.Contracts.RealTime;
using SyncLink.Application.Domain.Features.TextPlotGame;
using SyncLink.Server.Helpers;
using SyncLink.Server.Hubs;

internal class TextPlotGameNotificationService : ITextPlotGameNotificationService
{
    private readonly IHubContext<SyncLinkHub, ISyncLinkHub> _hubContext;

    public TextPlotGameNotificationService(IHubContext<SyncLinkHub, ISyncLinkHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public Task NotifyGameStartedAsync(int groupId, TextPlotGame game, CancellationToken cancellationToken)
    {
        var groupName = HubHelper.GetGroupNameForGroupId(groupId);
        return _hubContext.Clients.Group(groupName).GameStarted(game);
    }

    public Task NotifyNewEntryAsync(int groupId, TextPlotEntry entry, CancellationToken cancellationToken)
    {
        var groupName = HubHelper.GetGroupNameForGroupId(groupId);
        return _hubContext.Clients.Group(groupName).NewEntry(entry);
    }

    public Task NotifyVoteReceivedAsync(int groupId, TextPlotVote vote, CancellationToken cancellationToken)
    {
        var groupName = HubHelper.GetGroupNameForGroupId(groupId);
        return _hubContext.Clients.Group(groupName).VoteReceived(vote);
    }

    public Task NotifyGameEndedAsync(int groupId, TextPlotGame game, CancellationToken cancellationToken)
    {
        var groupName = HubHelper.GetGroupNameForGroupId(groupId);
        return _hubContext.Clients.Group(groupName).GameEnded(game);
    }
}
