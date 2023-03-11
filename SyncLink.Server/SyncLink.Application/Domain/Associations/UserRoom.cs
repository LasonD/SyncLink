using SyncLink.Common.Validation;

namespace SyncLink.Application.Domain.Associations;

public class UserRoom
{
    protected UserRoom() { }

    public UserRoom(User user, Room room)
    {
        User = user.GetValueOrThrowIfNull(nameof(user));
        Room = room.GetValueOrThrowIfNull(nameof(room));
        UserId = User.Id;
        RoomId = Room.Id;
    }

    public int UserId { get; private set; }
    public User User { get; private set; } = null!;

    public int RoomId { get; private set; }
    public Room Room { get; private set; } = null!;
}