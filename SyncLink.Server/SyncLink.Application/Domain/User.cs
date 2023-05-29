using SyncLink.Application.Domain.Associations;
using SyncLink.Application.Domain.Base;
using SyncLink.Common.Validation;

namespace SyncLink.Application.Domain;

public class User : EntityBase
{
    private readonly IList<UserGroup> _userGroups = new List<UserGroup>();
    private readonly IList<UserRoom> _userRooms = new List<UserRoom>();
    private readonly IList<Message> _messages = new List<Message>();
    private readonly IList<Language> _languages = new List<Language>();

    public User(string userName)
    {
        UserName = userName.GetValueOrThrowIfNullOrWhiteSpace(nameof(userName));
    }

    public string UserName { get; private set; }

    public void AddGroup(Group group, bool isCreator, bool isAdmin)
    {
        var userGroup = new UserGroup(this, group, isCreator, isAdmin);

        _userGroups.Add(userGroup);
    }

    public IReadOnlyCollection<UserGroup> UserGroups => _userGroups.AsReadOnly();

    public IReadOnlyCollection<UserRoom> UserRooms => _userRooms.AsReadOnly();

    public IReadOnlyCollection<Message> Messages => _messages.AsReadOnly();

    public IReadOnlyCollection<Language> Languages => _languages.AsReadOnly();
}