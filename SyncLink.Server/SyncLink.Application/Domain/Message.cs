using SyncLink.Data.Models;

namespace SyncLink.Application.Domain;

internal class Message : EntityBase
{
    public User Sender { get; private set; }

    public Room Room { get; private set; }

    public DateTime SentDateTime { get; private set; } 
}