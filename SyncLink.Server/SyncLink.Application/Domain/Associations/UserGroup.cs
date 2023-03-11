using SyncLink.Common.Validation;

namespace SyncLink.Application.Domain.Associations;

public class UserGroup
{
    protected UserGroup() { }

    public UserGroup(User user, Group group, bool isCreator = false, bool isAdmin = false) 
    {
        User = user.GetValueOrThrowIfNull(nameof(user));
        Group = group.GetValueOrThrowIfNull(nameof(group));
        UserId = User.Id;
        GroupId = Group.Id;
        IsCreator = isCreator;
        IsAdmin = isAdmin;
    }

    public bool IsCreator { get; set; }
    public bool IsAdmin { get; set; }

    public int UserId { get; private set; } = 0;
    public User User { get; private set; } = null!;

    public int GroupId { get; private set; } = 0;
    public Group Group { get; private set; } = null!;
}