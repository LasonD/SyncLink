using SyncLink.Application.Domain.Associations;

namespace SyncLink.Application.Domain;

public class Group : EntityBase
{
    public IReadOnlyCollection<UserGroup> UserGroups { get; protected set; } = null!;

    public IReadOnlyCollection<Room> Rooms { get; protected set; } = null!;
}