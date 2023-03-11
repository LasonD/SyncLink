using SyncLink.Application.Domain.Associations;

namespace SyncLink.Application.Domain;

public class User : EntityBase
{
    private readonly IList<UserGroup> _userGroups = null!;
    private readonly IList<UserRoom> _userRooms = null!;
    private readonly IList<Message> _messages = null!;

    public string UserName { get; private set; } = null!;

    public IReadOnlyCollection<UserGroup> UserGroups => _userGroups.AsReadOnly();

    public IReadOnlyCollection<UserRoom> UserRooms => _userRooms.AsReadOnly();

    public IReadOnlyCollection<Message> Messages => _messages.AsReadOnly();
}

