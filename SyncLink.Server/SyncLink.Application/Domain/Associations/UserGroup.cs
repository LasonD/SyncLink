namespace SyncLink.Application.Domain.Associations;

public class UserGroup
{
    public bool IsCreator { get; set; }
    public bool IsAdmin { get; set; }

    public int UserId { get; private set; } = 0;
    public User User { get; private set; } = null!;

    public int GroupId { get; private set; } = 0;
    public Group Group { get; private set; } = null!;
}