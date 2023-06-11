using SyncLink.Application.Domain.Base;
using SyncLink.Application.Domain.Groups.Rooms;
using SyncLink.Application.Exceptions;
using SyncLink.Common.Validation;

namespace SyncLink.Application.Domain.Groups;

public class Group : EntityBase
{
    private readonly IList<UserGroup> _userGroups = new List<UserGroup>();
    private readonly IList<Room> _rooms = new List<Room>();
    private readonly IList<GroupJoinRequest> _joinRequests = new List<GroupJoinRequest>();

    protected Group() { }

    public Group(string name, string? description, bool isPrivate)
    {
        Name = name.GetValueOrThrowIfNullOrWhiteSpace(nameof(name));
        Description = description;
        IsPrivate = isPrivate;
    }

    public void AddRoom(Room room)
    {
        room.ThrowIfNull(nameof(room));

        if (room.Group != null && room.Group != this || room.GroupId != default && room.GroupId != Id)
        {
            throw new InvalidOperationException($"Room {room.Name} {room.Id} is already assigned to a group.");
        }

        _rooms.Add(room);
    }

    public GroupJoinRequest AddJoinRequest(User user, string? message)
    {
        user.ThrowIfNull(nameof(user));

        var request = new GroupJoinRequest
        {
            Group = this,
            Message = message,
            User = user,
            Status = IsPrivate ? GroupJoinRequestStatus.Pending : GroupJoinRequestStatus.Accepted
        };

        _joinRequests.Add(request);

        if (request.Status == GroupJoinRequestStatus.Accepted)
        {
            _userGroups.Add(new UserGroup(user, this));
        }

        return request;
    }

    public void AcceptJoinRequest(GroupJoinRequest request)
    {
        request.ThrowIfNull(nameof(request));

        if (request.Status != GroupJoinRequestStatus.Pending)
        {
            throw new BusinessException($"Request {request.Id} is not pending.");
        }

        if (request.GroupId != Id)
        {
            throw new BusinessException($"Request {request.Id} is not for group {Id}.");
        }

        request.Status = GroupJoinRequestStatus.Accepted;
        _userGroups.Add(new UserGroup(request.User, this));
    }

    public string Name { get; private set; } = null!;

    public string? Description { get; private set; }

    public bool IsPrivate { get; private set; }

    public IReadOnlyCollection<UserGroup> UserGroups => _userGroups.AsReadOnly();

    public IReadOnlyCollection<Room> Rooms => _rooms.AsReadOnly();

    public IReadOnlyCollection<GroupJoinRequest> JoinRequests => _joinRequests.AsReadOnly();
}