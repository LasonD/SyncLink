using Microsoft.AspNetCore.SignalR;
using SyncLink.Application.Contracts.RealTime;
using SyncLink.Application.Dtos;
using SyncLink.Server.Helpers;
using SyncLink.Server.Hubs;

namespace SyncLink.Server.SignalR;

internal class GeneralGeneralNotificationsService : IGeneralNotificationsService
{
    private readonly IHubContext<SyncLinkHub, ISyncLinkHub> _hubContext;

    public GeneralGeneralNotificationsService(IHubContext<SyncLinkHub, ISyncLinkHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public Task NotifyMessageReceivedAsync(int groupId, int? roomId, int? otherUserId, bool isPrivate, MessageDto message, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var groupName = HubHelper.GetGroupNameForGroupId(groupId);

        return _hubContext.Clients.Group(groupName).MessageReceived(roomId, otherUserId, isPrivate, message);
    }
}