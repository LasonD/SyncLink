using SyncLink.Common.Validation;

namespace SyncLink.Application.Domain;

public class Message : EntityBase
{
    protected Message() { }

    public Message(User sender, Room room, string text)
    {
        sender.ThrowIfNull(nameof(sender));
        room.ThrowIfNull(nameof(room));

        Text = text;
    }

    public bool IsEdited { get; private set; } = false;

    public string Text { get; private set; } = null!;

    public int SenderId { get; private set; } = 0;
    public User Sender { get; private set; } = null!;

    public int RoomId { get; private set; } = 0;
    public Room Room { get; private set; } = null!;
}