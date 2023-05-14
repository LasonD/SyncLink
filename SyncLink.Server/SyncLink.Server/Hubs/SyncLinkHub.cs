using Microsoft.AspNetCore.SignalR;
using SyncLink.Application.Dtos;

namespace SyncLink.Server.Hubs;

public interface ISyncLinkHub
{
    Task MessageReceived(MessageDto message);
}

public class SyncLinkHub : Hub<ISyncLinkHub>
{

}