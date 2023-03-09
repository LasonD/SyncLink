using SyncLink.Data.Models;

namespace SyncLink.Application.Domain;

public class Room : EntityBase
{
    public IReadOnlyCollection<User> Members { get; protected set; } = null!;

    public IReadOnlyCollection<Message> Messages { get; protected set; } = null!;
}