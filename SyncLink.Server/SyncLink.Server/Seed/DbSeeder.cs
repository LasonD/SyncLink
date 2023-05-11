using Microsoft.AspNetCore.Identity;
using SyncLink.Application.Domain;
using SyncLink.Infrastructure.Data.Context;
using SyncLink.Infrastructure.Data.Models.Identity;

namespace SyncLink.Server.Seed;

public static class DbSeeder
{
    public static async Task SeedAsync(IServiceProvider servicesProvider)
    {
        await using var scope = servicesProvider.CreateAsyncScope();
        await using var context = scope.ServiceProvider.GetRequiredService<SyncLinkDbContext>();

        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();

        var hasher = new PasswordHasher<SyncLinkIdentityUser>();

        var applicationUsers = new List<User>()
        {
            new User("alex_peterson"),
            new User("jane_doe"),
            new User("lucy_lark"),
            new User("william_mcCart"),
            new User("Host"),
        };

        var users = new SyncLinkIdentityUser[]
        {
            new()
            {
                Id = Guid.NewGuid().ToString(),
                Email = "admin@localhost.com",
                NormalizedEmail = "admin@localhost.com".ToUpperInvariant(),
                NormalizedUserName = "admin@localhost.com".ToUpperInvariant(),
                PasswordHash = hasher.HashPassword(null, "123adm"),
                EmailConfirmed = true,
                FirstName = "Alex",
                LastName = "Peterson",
                ApplicationUser = applicationUsers[0],
            },
            new()
            {
                Id = Guid.NewGuid().ToString(),
                Email = "admin2@localhost.com",
                NormalizedEmail = "admin2@localhost.com".ToUpperInvariant(),
                NormalizedUserName = "admin2@localhost.com".ToUpperInvariant(),
                PasswordHash = hasher.HashPassword(null, "123adm"),
                EmailConfirmed = true,
                FirstName = "Jane",
                LastName = "Doe",
                ApplicationUser = applicationUsers[1],
            },
            new()
            {
                Id = Guid.NewGuid().ToString(),
                Email = "user@localhost.com",
                NormalizedEmail = "user@localhost.com".ToUpperInvariant(),
                NormalizedUserName = "user@localhost.com".ToUpperInvariant(),
                PasswordHash = hasher.HashPassword(null, "123usr"),
                EmailConfirmed = true,
                FirstName = "Lucy",
                LastName = "Lark",
                ApplicationUser = applicationUsers[2],
            },
            new()
            {
                Id = Guid.NewGuid().ToString(),
                Email = "user2@localhost.com",
                NormalizedEmail = "user2@localhost.com".ToUpperInvariant(),
                NormalizedUserName = "user2@localhost.com".ToUpperInvariant(),
                PasswordHash = hasher.HashPassword(null, "1234usr"),
                EmailConfirmed = true,
                FirstName = "William",
                LastName = "McCart",
                ApplicationUser = applicationUsers[3],
            },
            new()
            {
                Id = Guid.NewGuid().ToString(),
                Email = "sbarylo20023@gmail.com",
                NormalizedEmail = "sbarylo20023@gmail.com".ToUpperInvariant(),
                NormalizedUserName = "sbarylo20023@gmail.com".ToUpperInvariant(),
                PasswordHash = hasher.HashPassword(null, "12345"),
                EmailConfirmed = true,
                FirstName = "Oleksandr",
                LastName = "Barylo",
                ApplicationUser = applicationUsers[4],
            }
        };

        var groups = new Group[]
        {
            new Group("Group1", "This is a test group 1", false),
            new Group("Group2", "This is a test group 2", true),
        };

        applicationUsers[4].AddGroup(groups[0], true, true);
        applicationUsers[0].AddGroup(groups[0], false, false);
        applicationUsers[2].AddGroup(groups[0], false, false);
        applicationUsers[1].AddGroup(groups[1], true, true);
        applicationUsers[3].AddGroup(groups[1], false, false);
        applicationUsers[1].AddGroup(groups[0], false, false);

        var rooms = new Room[]
        {
            new Room("Room1_1", new User[] { applicationUsers[0], applicationUsers[1] }),
            new Room("Room1_2", new User[] { applicationUsers[0], applicationUsers[1], applicationUsers[2], applicationUsers[4] }),
            new Room("Room1_3", new User[] { applicationUsers[0], applicationUsers[1], applicationUsers[2] }),
            new Room("Room1_4", new User[] { applicationUsers[0], applicationUsers[4] }),
            new Room("Room2_1", new User[] { applicationUsers[1], applicationUsers[3] })
        };

        groups[0].AddRoom(rooms[0]);
        groups[0].AddRoom(rooms[1]);
        groups[0].AddRoom(rooms[2]);
        groups[0].AddRoom(rooms[3]);
        groups[1].AddRoom(rooms[4]);

        var messages = new Message[]
        {
            new Message(applicationUsers[0],  rooms[0], "Message 1 to room 1"),
            new Message(applicationUsers[1],  rooms[0], "Message 2 to room 1"),
            new Message(applicationUsers[1],  rooms[1], "Message 1 to room 2"),
            new Message(applicationUsers[1],  rooms[1], "Message 2 to room 2"),
            new Message(applicationUsers[2],  rooms[1], "Message 3 to room 2"),
            new Message(applicationUsers[1],  rooms[1], "Message 4 to room 2"),
            new Message(applicationUsers[4],  rooms[3], "Message 4 to room 2"),
            new Message(applicationUsers[4],  rooms[1], "Message 4 to room 2"),
        };

        await context.Users.AddRangeAsync(users);
        await context.Groups.AddRangeAsync(groups);
        await context.Rooms.AddRangeAsync(rooms);
        await context.Messages.AddRangeAsync(messages);

        await context.SaveChangesAsync();
    }
}