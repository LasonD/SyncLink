namespace SyncLink.Application.Domain;

public class Message : EntityBase
{
    public int SenderId { get; private set; } = 0;
    public User Sender { get; protected set; } = null!;

    public int RoomId { get; private set; } = 0;
    public Room Room { get; protected set; } = null!;

    public bool IsEdited { get; private set; } = false;
}