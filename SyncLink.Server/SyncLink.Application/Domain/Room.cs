using SyncLink.Data.Models;

namespace SyncLink.Application.Domain;

public class Room : EntityBase
{
    private List<User> Members { get; set; }

    private List<Message> Messages { get; set; }
}