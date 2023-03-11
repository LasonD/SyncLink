using SyncLink.Application.Domain.Associations;

namespace SyncLink.Application.Domain;

public class User : EntityBase
{
    public string UserName { get; private set; } = null!;

    public IReadOnlyCollection<UserGroup> UserGroups { get; private set; } = null!;

    public IReadOnlyCollection<UserRoom> UserRooms { get; private set; } = null!;

    public IReadOnlyCollection<Message> Messages { get; private set; } = null!;
}

