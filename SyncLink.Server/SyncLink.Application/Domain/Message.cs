using SyncLink.Application.Domain.Base;
using SyncLink.Common.Validation;

namespace SyncLink.Application.Domain;

public class Message : EntityBase
{
    protected Message() { }

    public Message(User sender, Room room, string text)
    {
        Sender = sender.GetValueOrThrowIfNull(nameof(sender));
        Room = room.GetValueOrThrowIfNull(nameof(room));
        Text = text.GetValueOrThrowIfNullOrWhiteSpace(nameof(text));
    }

    public bool Edit(string newText)
    {
        newText.ThrowIfNullOrWhiteSpace(nameof(newText));

        if (Text == newText)
        {
            return false;
        }

        Text = newText;
        EditedDateTime = DateTime.UtcNow;

        return true;
    }

    public DateTime EditedDateTime { get; private set; } = default;

    public bool IsEdited { get; private set; } = false;

    public string Text { get; private set; } = null!;

    public int SenderId { get; private set; } = 0;
    public User Sender { get; private set; } = null!;

    public int RoomId { get; private set; } = 0;
    public Room Room { get; private set; } = null!;
}