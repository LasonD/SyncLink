using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SyncLink.Infrastructure.Data.Models.Identity;

namespace SyncLink.Infrastructure.Data.Context;

public class SyncLinkDbContext : IdentityDbContext<SyncLinkIdentityUser>
{
    public SyncLinkDbContext(DbContextOptions<SyncLinkDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // builder.Entity<User>()
        //     .HasData()
    }
}
