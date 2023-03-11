namespace SyncLink.Application.Domain.Associations;

public class UserRoom
{
    public int UserId { get; private set; } = 0;
    public User User { get; private set; } = null!;

    public int RoomId { get; private set; } = 0;
    public Room Room { get; private set; } = null!;
}