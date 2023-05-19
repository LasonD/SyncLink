using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SyncLink.Application.Domain;
using SyncLink.Application.Domain.Associations;
using SyncLink.Application.Domain.Features;
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

    public DbSet<Group> Groups { get; protected set; } = null!;

    public DbSet<User> ApplicationUsers { get; set; } = null!;

    public DbSet<UserGroup> UsersToGroups { get; set; } = null!;

    public DbSet<UserRoom> UsersToRooms { get; set; } = null!;

    public DbSet<Whiteboard> Whiteboards { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
