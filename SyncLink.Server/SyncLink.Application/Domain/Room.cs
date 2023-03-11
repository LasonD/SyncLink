using SyncLink.Application.Domain.Associations;

namespace SyncLink.Application.Domain;

public class Room : EntityBase
{
    private readonly IList<UserRoom> _roomMembers = null!;
    private readonly IList<Message> _messages = null!;

    public IReadOnlyCollection<UserRoom> RoomMembers => _roomMembers.AsReadOnly();

    public IReadOnlyCollection<Message> Messages => _messages.AsReadOnly();

    public int GroupId { get; private set; } = 0;
    public Group Group { get; private set; } = null!;
}