using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Dtos;
using SyncLink.Application.Dtos.TextPlotGame;
using SyncLink.Application.Dtos.WordsChainGame;
using SyncLink.Application.Exceptions;
using SyncLink.Application.UseCases.Features.Whiteboard.Commands;
using SyncLink.Common.Helpers.Jwt;
using SyncLink.Server.Helpers;

namespace SyncLink.Server.Hubs;

public interface ISyncLinkHub
{
    #region General
    Task MessageReceived(int? roomId, int? otherUserId, bool isPrivate, MessageDto message);
    Task NotifyJoinGroupRequestReceived(GroupJoinRequestDto groupJoinRequest);
    #endregion

    #region Whiteboard
    Task BoardUpdated(int groupId, int whiteboardId, WhiteboardElementDto[] change);
    #endregion

    #region TextPlotGame
    Task GameStarted(TextPlotGameDto game);
    Task NewEntry(TextPlotEntryDto entry);
    Task EntryCommitted(TextPlotEntryDto entry);
    Task EntryNotCommitted(int gameId);
    Task EntriesDiscarded(int[] discardedEntryIds);
    Task VoteReceived(TextPlotVoteDto vote);
    Task VoteRevoked(int gameId, int voteId);
    Task GameEnded(TextPlotGameDto game);
    Task VotingTimerProgress(int gameId, double percent);
    #endregion

    #region WordsChainGame
    Task NewWordsChainGame(WordsChainGameOverviewDto game);
    Task NewEntry(WordsChainGameEntryDto entry);
    #endregion
}

[Authorize]
public class SyncLinkHub : Hub<ISyncLinkHub>
{
    private readonly IUserRepository _userRepository;
    private readonly IMediator _mediator;

    public SyncLinkHub(IUserRepository userRepository, IMediator mediator)
    {
        _userRepository = userRepository;
        _mediator = mediator;
    }

    #region General

    public async Task GroupOpened(int groupId)
    {
        var isUserInGroup = await _userRepository.IsUserInGroupAsync(UserId, groupId, CancellationToken.None);

        if (!isUserInGroup)
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

    public override Task OnConnectedAsync()
    {
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        return base.OnDisconnectedAsync(exception);
    }

    #endregion

    #region Whiteboard

    public async Task BoardUpdated(int groupId, int whiteboardId, string updateJson)
    {
        try
        {
            var update = JsonConvert.DeserializeObject<WhiteboardElementDto[]>(updateJson);

            var command = new UpdateWhiteboard.Command
            {
                GroupId = groupId,
                Update = update?.ToArray()!,
                UserId = UserId,
                WhiteboardId = whiteboardId
            };

            var result = await _mediator.Send(command);

            await Clients.GroupExcept(GetGroupNameByGroupId(groupId), new[] { ConnectionId }).BoardUpdated(groupId, whiteboardId, result);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    #endregion

    #region TextPlotGame

    public async Task EndGame()
    {

    }

    #endregion

    #region Utils

    protected string GetGroupNameByGroupId(int groupId) => HubHelper.GetGroupNameForGroupId(groupId);

    protected string ConnectionId => Context.ConnectionId;

    protected int UserId => AppUserIdClaimHelper.RetrieveUserId(Context.User!) ?? throw new AuthException(new[] { "User id should be present in a hub." });

    #endregion
}