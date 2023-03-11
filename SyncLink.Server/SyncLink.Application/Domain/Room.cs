using SyncLink.Application.Domain.Associations;
using SyncLink.Application.Domain.Base;

namespace SyncLink.Application.Domain;

public class Room : EntityBase
{
    private readonly IList<UserRoom> _roomMembers = null!;
    private readonly IList<Message> _messages = null!;

    protected Room() { }

    public Room(string? name = null)
    {
        Name = name;
    }

    public string? Name { get; private set; }

    public IReadOnlyCollection<UserRoom> RoomMembers => _roomMembers.AsReadOnly();

    public IReadOnlyCollection<Message> Messages => _messages.AsReadOnly();

    public int GroupId { get; private set; } = 0;
    public Group Group { get; private set; } = null!;
}