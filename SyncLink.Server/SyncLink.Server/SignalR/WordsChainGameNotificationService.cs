using Microsoft.AspNetCore.SignalR;
using SyncLink.Application.Contracts.RealTime;
using SyncLink.Application.Dtos.WordsChainGame;
using SyncLink.Server.Helpers;
using SyncLink.Server.Hubs;

namespace SyncLink.Server.SignalR;

public class WordsChainGameNotificationService : IWordsChainGameNotificationService
{
    private readonly IHubContext<SyncLinkHub, ISyncLinkHub> _hubContext;

    public Task NotifyNewEntryAsync(int groupId, WordsChainGameEntryDto entry, CancellationToken cancellationToken)
    {
        var groupName = HubHelper.GetGroupNameForGroupId(groupId);
        return _hubContext.Clients.Group(groupName).NewEntry(entry);
    }

    public Task NotifyNewWordsChainGameCreatedAsync(int groupId, WordsChainGameOverviewDto game, CancellationToken cancellationToken)
    {
        var groupName = HubHelper.GetGroupNameForGroupId(groupId);
        return _hubContext.Clients.Group(groupName).NewWordsChainGame(game);
    }
}