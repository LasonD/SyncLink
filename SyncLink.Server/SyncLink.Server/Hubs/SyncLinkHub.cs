using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Dtos;
using SyncLink.Application.Exceptions;
using SyncLink.Common.Helpers.Jwt;
using SyncLink.Server.Helpers;

namespace SyncLink.Server.Hubs;

public interface ISyncLinkHub
{
    Task MessageReceived(int? roomId, int? otherUserId, bool isPrivate, MessageDto message);
}

[Authorize]
public class SyncLinkHub : Hub<ISyncLinkHub>
{
    private readonly IUserRepository _userRepository;

    public SyncLinkHub(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task GroupOpened(int groupId)
    {
        var isUserInGroup = await _userRepository.IsUserInGroupAsync(UserId, groupId, CancellationToken.None);

        if (isUserInGroup)
        {
            throw new AuthException(new[] { $"User {UserId} has no access to group {groupId}" });
        }

        var groupName = HubHelper.GetGroupNameForGroupId(groupId);

        await Groups.AddToGroupAsync(ConnectionId, groupName);
    }

    public Task GroupClosed(int groupId)
    {
        var groupName = HubHelper.GetGroupNameForGroupId(groupId);

        return Groups.RemoveFromGroupAsync(ConnectionId, groupName);
    }

    protected string ConnectionId => Context.ConnectionId;

    protected int UserId => AppUserIdClaimHelper.RetrieveUserId(Context.User!) ?? throw new AuthException(new[] { "User id should be present in a hub." });

    public override Task OnConnectedAsync()
    {
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        return base.OnDisconnectedAsync(exception);
    }
}