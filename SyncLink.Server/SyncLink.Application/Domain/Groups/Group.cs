using SyncLink.Application.Domain.Base;
using SyncLink.Application.Domain.Groups.Rooms;
using SyncLink.Common.Validation;

namespace SyncLink.Application.Domain.Groups;

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

    public void AddRoom(Room room)
    {
        room.ThrowIfNull(nameof(room));

        if (room.Group != null && room.Group != this || room.GroupId != default && room.GroupId != Id)
        {
            throw new InvalidOperationException($"Room {room.Name} {room.Id} is already assigned to a group.");
        }

        _rooms.Add(room);
    }

    public string Name { get; private set; } = null!;

    public string? Description { get; private set; }

    public bool IsPrivate { get; private set; }

    public IReadOnlyCollection<UserGroup> UserGroups => _userGroups.AsReadOnly();

    public IReadOnlyCollection<Room> Rooms => _rooms.AsReadOnly();
}