using Microsoft.AspNetCore.Identity;

namespace SyncLink.Infrastructure.Data.Models.Identity;

public class SyncLinkIdentityUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

