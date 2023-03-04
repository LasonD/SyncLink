using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SyncLink.Data.Models.Identity;

namespace SyncLink.Data.Context;

public class SyncLinkDbContext : IdentityDbContext<User>
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
