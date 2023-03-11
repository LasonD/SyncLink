namespace SyncLink.Application.Domain;

public class Message : EntityBase
{
    public bool IsEdited { get; private set; } = false;

    public int SenderId { get; private set; } = 0;
    public User Sender { get; private set; } = null!;

    public int RoomId { get; private set; } = 0;
    public Room Room { get; private set; } = null!;
}