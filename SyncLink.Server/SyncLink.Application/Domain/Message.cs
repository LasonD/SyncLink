using SyncLink.Data.Models;

namespace SyncLink.Application.Domain;

public class Message : EntityBase
{
    public User Sender { get; protected set; } = null!;

    public Room Room { get; protected set; } = null!;

    public DateTime SentDateTime { get; protected set; } 
}