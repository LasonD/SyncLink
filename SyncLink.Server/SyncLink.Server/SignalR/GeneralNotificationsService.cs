using Microsoft.AspNetCore.SignalR;
using SyncLink.Application.Contracts.RealTime;
using SyncLink.Application.Dtos;
using SyncLink.Server.Helpers;
using SyncLink.Server.Hubs;

namespace SyncLink.Server.SignalR;

internal class GeneralNotificationsService : IGeneralNotificationsService
{
    private readonly IHubContext<SyncLinkHub, ISyncLinkHub> _hubContext;

    public GeneralNotificationsService(IHubContext<SyncLinkHub, ISyncLinkHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public Task NotifyMessageReceivedAsync(int groupId, int? roomId, int? otherUserId, bool isPrivate, MessageDto message, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var groupName = HubHelper.GetGroupNameForGroupId(groupId);
        return _hubContext.Clients.Group(groupName).MessageReceived(roomId, otherUserId, isPrivate, message);
    }

    public Task NotifyJoinGroupRequestCreatedOrUpdatedAsync(int groupId, GroupJoinRequestDto groupJoinRequest, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var groupName = HubHelper.GetGroupNameForGroupId(groupId);
        return _hubContext.Clients.Group(groupName).NotifyJoinGroupRequestReceived(groupJoinRequest);
    }
}