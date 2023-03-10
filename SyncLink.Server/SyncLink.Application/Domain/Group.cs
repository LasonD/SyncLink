using SyncLink.Application.Domain.Associations;
using SyncLink.Application.Domain.Base;
using SyncLink.Common.Validation;

namespace SyncLink.Application.Domain;

public class Group : EntityBase
{
    private readonly IList<UserGroup> _userGroups = null!;
    private readonly IList<Room> _rooms = null!;

    protected Group() { }

    public Group(string name, string? description)
    {
        Name = name.GetValueOrThrowIfNullOrWhiteSpace(nameof(name));
        Description = description;
    }

    public string Name { get; private set; } = null!;

    public string? Description { get; private set; }

    public IReadOnlyCollection<UserGroup> UserGroups => _userGroups.AsReadOnly();

    public IReadOnlyCollection<Room> Rooms => _rooms.AsReadOnly();
}