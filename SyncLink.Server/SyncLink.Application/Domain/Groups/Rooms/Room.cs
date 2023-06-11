using SyncLink.Application.Domain.Base;
using SyncLink.Application.Domain.Groups;
using SyncLink.Application.Exceptions;
using SyncLink.Common.Validation;

namespace SyncLink.Application.Domain.Groups.Rooms;

public class Room : EntityBase
{
    private readonly IList<UserRoom> _roomMembers = new List<UserRoom>();
    private readonly IList<Message> _messages = new List<Message>();

    protected Room() { }

    public Room(string? name, string? description, int groupId, User creator, IEnumerable<User> members)
    {
        members.ThrowIfNull(nameof(members));
        Name = name;
        Description = description;
        AddMembers(members);
        AddMember(creator, isAdmin: true);
        IsPrivate = false;
        GroupId = groupId;
    }

    public Room(int groupId, User firstMember, User secondMember)
    {
        firstMember.ThrowIfNull(nameof(firstMember));
        secondMember.ThrowIfNull(nameof(secondMember));
        Name = null;
        AddMembers(new List<User> { firstMember, secondMember });
        IsPrivate = true;
        GroupId = groupId;
    }

    public string? Name { get; private set; }
    public string? Description { get; private set; }

    public void AddMembers(IEnumerable<User> users)
    {
        var usersList = users.ToList();

        if (IsPrivate && usersList.Count > 2)
        {
            throw new BusinessException("Private group cannot have more than 2 members.");
        }

        foreach (var user in usersList)
        {
            AddMember(user);
        }
    }

    public void AddMember(User user, bool isAdmin = false)
    {
        user.ThrowIfNull(nameof(user));

        var userRoom = new UserRoom(user, this, isAdmin);

        _roomMembers.Add(userRoom);
    }

    public IReadOnlyCollection<UserRoom> RoomMembers => _roomMembers.AsReadOnly();

    public IReadOnlyCollection<Message> Messages => _messages.AsReadOnly();

    public bool IsPrivate { get; private set; } = false;

    public int GroupId { get; private set; } = 0;
    public Group Group { get; private set; } = null!;
}