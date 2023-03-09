using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SyncLink.Application.Domain;
using SyncLink.Infrastructure.Data.Models.Identity;

namespace SyncLink.Infrastructure.Data.Context;

public class SyncLinkDbContext : IdentityDbContext<SyncLinkIdentityUser>
{
    public SyncLinkDbContext(DbContextOptions<SyncLinkDbContext> options)
        : base(options)
    {
    }

    public DbSet<Message> Messages { get; protected set; } = null!;

    public DbSet<Room> Rooms { get; protected set; } = null!;

    public DbSet<Group> Group { get; protected set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // builder.Entity<User>()
        //     .HasData()
    }
}
