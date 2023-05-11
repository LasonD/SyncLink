using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SyncLink.Application.Domain;
using SyncLink.Application.Domain.Associations;
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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // var hasher = new PasswordHasher<SyncLinkIdentityUser>();
        //
        // var applicationUsers = new List<User>()
        // {
        //     new User("alex_peterson") { Id = 1 },
        //     new User("jane_doe") { Id = 2 },
        //     new User("lucy_lark") { Id = 3 },
        //     new User("william_mcCart") { Id = 4 }
        // };
        //
        // var users = new SyncLinkIdentityUser[]
        // {
        //     new()
        //     {
        //         Id = Guid.NewGuid().ToString(),
        //         Email = "admin@localhost.com",
        //         NormalizedEmail = "admin@localhost.com".ToUpperInvariant(),
        //         NormalizedUserName = "admin@localhost.com".ToUpperInvariant(),
        //         PasswordHash = hasher.HashPassword(null, "123adm"),
        //         EmailConfirmed = true,
        //         FirstName = "Alex",
        //         LastName = "Peterson",
        //         ApplicationUser = applicationUsers[0],
        //     },
        //     new()
        //     {
        //         Id = Guid.NewGuid().ToString(),
        //         Email = "admin2@localhost.com",
        //         NormalizedEmail = "admin2@localhost.com".ToUpperInvariant(),
        //         NormalizedUserName = "admin2@localhost.com".ToUpperInvariant(),
        //         PasswordHash = hasher.HashPassword(null, "123adm"),
        //         EmailConfirmed = true,
        //         FirstName = "Jane",
        //         LastName = "Doe",
        //         ApplicationUser = applicationUsers[1],
        //     },
        //     new()
        //     {
        //         Id = Guid.NewGuid().ToString(),
        //         Email = "user@localhost.com",
        //         NormalizedEmail = "user@localhost.com".ToUpperInvariant(),
        //         NormalizedUserName = "user@localhost.com".ToUpperInvariant(),
        //         PasswordHash = hasher.HashPassword(null, "123usr"),
        //         EmailConfirmed = true,
        //         FirstName = "Lucy",
        //         LastName = "Lark",
        //         ApplicationUser = applicationUsers[2],
        //     },
        //     new()
        //     {
        //         Id = Guid.NewGuid().ToString(),
        //         Email = "user2@localhost.com",
        //         NormalizedEmail = "user2@localhost.com".ToUpperInvariant(),
        //         NormalizedUserName = "user2@localhost.com".ToUpperInvariant(),
        //         PasswordHash = hasher.HashPassword(null, "1234usr"),
        //         EmailConfirmed = true,
        //         FirstName = "William",
        //         LastName = "McCart",
        //         ApplicationUser = applicationUsers[3],
        //     }
        // };
        //
        // var groups = new Group[]
        // {
        //     new Group("Group1", "This is a test group 1", false) { Id = 1 },
        //     new Group("Group2", "This is a test group 2", true) { Id = 2 },
        // };
        //
        // applicationUsers[0].AddGroup(groups[0], true, true);
        // applicationUsers[2].AddGroup(groups[0], false, false);
        // applicationUsers[1].AddGroup(groups[1], true, true);
        // applicationUsers[3].AddGroup(groups[1], false, false);
        // applicationUsers[1].AddGroup(groups[0], false, false);
        //
        // var rooms = new Room[]
        // {
        //     new Room("Room1_1", new User[] { applicationUsers[0], applicationUsers[1] })
        //     {
        //         Id = 1,
        //         GroupId = 1,
        //     },
        //     new Room("Room1_2", new User[] { applicationUsers[0], applicationUsers[1], applicationUsers[2] })
        //     {
        //         Id = 2,
        //         GroupId = 1,
        //     },
        //     new Room("Room2_1", new User[] { applicationUsers[1], applicationUsers[3] })
        //     {
        //         Id = 3,
        //         GroupId = 2,
        //     }
        // };
        //
        // // groups[0].AddRoom(rooms[0]);
        // // groups[0].AddRoom(rooms[1]);
        // // groups[1].AddRoom(rooms[2]);
        //
        // var messages = new Message[]
        // {
        //     new Message(applicationUsers[0],  rooms[0], "Message 1 to room 1")
        //     {
        //         Id = 1,
        //         SenderId = 1,
        //         RoomId = 1,
        //     },
        //     new Message(applicationUsers[1],  rooms[0], "Message 2 to room 1")
        //     {
        //         Id = 2,
        //         SenderId = 2,
        //         RoomId = 1,
        //     },
        //     new Message(applicationUsers[1],  rooms[1], "Message 1 to room 2")
        //     {
        //         Id = 3,
        //         SenderId = 2,
        //         RoomId = 2,
        //     },
        //     new Message(applicationUsers[1],  rooms[1], "Message 2 to room 2")
        //     {
        //         Id = 4,
        //         SenderId = 2,
        //         RoomId = 2,
        //     },
        //     new Message(applicationUsers[2],  rooms[1], "Message 3 to room 2")
        //     {
        //         Id = 5,
        //         SenderId = 3,
        //         RoomId = 2,
        //     },
        //     new Message(applicationUsers[1],  rooms[1], "Message 4 to room 2")
        //     {
        //         Id = 6,
        //         SenderId = 2,
        //         RoomId = 2,
        //     },
        // };
        //
        // builder.Entity<SyncLinkIdentityUser>()
        //     .HasData(users);
        //
        // builder.Entity<Group>()
        //     .HasData(groups);
        //
        // builder.Entity<Room>()
        //     .HasData(rooms);
        //
        // builder.Entity<Message>()
        //     .HasData(messages);
    }
}
