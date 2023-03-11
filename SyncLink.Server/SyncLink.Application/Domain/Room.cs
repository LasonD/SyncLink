using SyncLink.Application.Domain.Associations;

namespace SyncLink.Application.Domain;

public class Room : EntityBase
{
    public IReadOnlyCollection<UserRoom> RoomMembers { get; protected set; } = null!;

    public IReadOnlyCollection<Message> Messages { get; protected set; } = null!;

    public int GroupId { get; private set; } = 0;
    public Group Group { get; private set; } = null!;
}