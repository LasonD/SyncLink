using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SyncLink.Application.Dtos;

namespace SyncLink.Server.Hubs;

public interface ISyncLinkHub
{
    Task MessageReceived(int? roomId, int? otherUserId, bool isPrivate, MessageDto message);
}

[Authorize]
public class SyncLinkHub : Hub<ISyncLinkHub>
{
    public override Task OnConnectedAsync()
    {
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        return base.OnDisconnectedAsync(exception);
    }
}