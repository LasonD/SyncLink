using SyncLink.Application.Domain.Associations;
using SyncLink.Application.Domain.Base;
using SyncLink.Application.Exceptions;
using SyncLink.Common.Validation;

namespace SyncLink.Application.Domain;

public class Room : EntityBase
{
    private readonly IList<UserRoom> _roomMembers = new List<UserRoom>();
    private readonly IList<Message> _messages = new List<Message>();

    protected Room() { }

    public Room(string? name, IEnumerable<User> users)
    {
        users.ThrowIfNull(nameof(users));
        Name = name;
        AddMembers(users);
    }

    public static Room CreatePrivate(User firstMember, User secondMember)
    {
        firstMember.ThrowIfNull(nameof(firstMember));
        secondMember.ThrowIfNull(nameof(secondMember));

        return new Room(null, new List<User> { firstMember, secondMember });
    }

    public string? Name { get; private set; }

    public void AddMessage(Message message)
    {
        message.ThrowIfNull(nameof(message));

        _messages.Add(message);
    }

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

    public void AddMember(User user)
    {
        user.ThrowIfNull(nameof(user));

        var userRoom = new UserRoom(user, this);

        _roomMembers.Add(userRoom);
    }

    public IReadOnlyCollection<UserRoom> RoomMembers => _roomMembers.AsReadOnly();

    public IReadOnlyCollection<Message> Messages => _messages.AsReadOnly();

    public bool IsPrivate { get; private set; }

    public int GroupId { get; private set; } = 0;
    public Group Group { get; private set; } = null!;
}