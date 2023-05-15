﻿using Microsoft.AspNetCore.SignalR;
using SyncLink.Application.Contracts.RealTime;
using SyncLink.Application.Dtos;
using SyncLink.Server.Helpers;
using SyncLink.Server.Hubs;

namespace SyncLink.Server.SignalR;

internal class NotificationService : INotificationsService
{
    private readonly IHubContext<SyncLinkHub, ISyncLinkHub> _hubContext;

    public NotificationService(IHubContext<SyncLinkHub, ISyncLinkHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public Task NotifyMessageReceivedAsync(int groupId, int? roomId, int? otherUserId, bool isPrivate, MessageDto message, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var groupName = HubHelper.GetGroupNameForGroupId(groupId);

        return _hubContext.Clients.All.MessageReceived(roomId, otherUserId, isPrivate, message);
    }
}