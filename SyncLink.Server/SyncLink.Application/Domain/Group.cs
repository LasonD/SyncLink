using SyncLink.Application.Domain.Associations;
using SyncLink.Application.Domain.Base;
using SyncLink.Common.Validation;

namespace SyncLink.Application.Domain;

public class Group : EntityBase
{
    private readonly IList<UserGroup> _userGroups = new List<UserGroup>();
    private readonly IList<Room> _rooms = new List<Room>();

    protected Group() { }

    public Group(string name, string? description, bool isPrivate)
    {
        Name = name.GetValueOrThrowIfNullOrWhiteSpace(nameof(name));
        Description = description;
        IsPrivate = isPrivate;
    }

    public string Name { get; private set; } = null!;

    public string? Description { get; private set; }

    public bool IsPrivate { get; private set; }

    public void AddUser(User user, bool isAdmin = false)
    {
        user.ThrowIfNull(nameof(user));

        var userGroup = new UserGroup(user, this, isCreator: false, isAdmin);

        _userGroups.Add(userGroup);
    }

    public void AddRoom(Room room)
    {
        room.ThrowIfNull(nameof(room));

        _rooms.Add(room);
    }

    public IReadOnlyCollection<UserGroup> UserGroups => _userGroups.AsReadOnly();

    public IReadOnlyCollection<Room> Rooms => _rooms.AsReadOnly();
}