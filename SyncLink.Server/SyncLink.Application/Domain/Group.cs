using SyncLink.Application.Domain.Associations;

namespace SyncLink.Application.Domain;

public class Group : EntityBase
{
    private readonly IList<UserGroup> _userGroups = null!;
    private readonly IList<Room> _rooms = null!;

    public IReadOnlyCollection<UserGroup> UserGroups => _userGroups.AsReadOnly();

    public IReadOnlyCollection<Room> Rooms => _rooms.AsReadOnly();
}