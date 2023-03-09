using SyncLink.Data.Models;

namespace SyncLink.Application.Domain;

public class Group : EntityBase
{
    public IReadOnlyCollection<User> Users { get; protected set; } = null!;

    public IReadOnlyCollection<Room> Rooms { get; protected set; } = null!;
}