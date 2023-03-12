using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SyncLink.Application.Domain;
using SyncLink.Infrastructure.Data.Models.Identity;
using System.Reflection;

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

    public DbSet<User> ApplicationUsers { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // var rooms = new Room[]
        // {
        //     new()
        //     {
        //
        //     }
        // };
        //
        // var identities = new SyncLinkIdentityUser[]
        // {
        //     new()
        //     {
        //         Email = "test1@gmail.com",
        //         UserName = "DavidBRakhum",
        //         FirstName = "David",
        //         LastName = "Brakhum",
        //         ApplicationUser = new User()
        //         {
        //             Id = 1,
        //         }
        //     }
        // };
        //
        // builder.Entity<SyncLinkIdentityUser>()
        //     .HasData(identities);
    }
}
