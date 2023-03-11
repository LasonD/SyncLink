using Microsoft.AspNetCore.Identity;
using SyncLink.Application.Domain;

namespace SyncLink.Infrastructure.Data.Models.Identity;

public class SyncLinkIdentityUser : IdentityUser
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public int ApplicationUserId { get; set; } = 0;
    public User ApplicationUser { get; set; } = null!;
}

