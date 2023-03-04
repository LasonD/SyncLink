using SyncLink.Data.Models;

namespace SyncLink.Application.Domain;

public class User : EntityBase
{
    public string UserName { get; private set; }
}

