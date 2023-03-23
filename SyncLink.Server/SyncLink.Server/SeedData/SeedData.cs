// using Microsoft.EntityFrameworkCore;
// using SyncLink.Application.Domain.Associations;
// using SyncLink.Application.Domain;
//
// public static class SeedData
// {
//     public static void Seed(ModelBuilder modelBuilder)
//     {
//         var user1 = new User("john_doe");
//         var user2 = new User("jane_doe");
//         var user3 = new User("bob_smith");
//         var user4 = new User("alice_smith");
//
//         modelBuilder.Entity<User>().HasData(user1, user2, user3, user4);
//
//         var group1 = new Group("Family", "A group for family members", false);
//         var group2 = new Group("Work", "A group for work colleagues", true);
//
//         modelBuilder.Entity<Group>().HasData(group1, group2);
//
//         group1.Add
//
//         var userGroup1 = new UserGroup(user1, group1, true, true);
//         var userGroup2 = new UserGroup(user2, group1, false, true);
//         var userGroup3 = new UserGroup(user3, group2, true, true);
//         var userGroup4 = new UserGroup(user4, group2, false, true);
//
//         modelBuilder.Entity<UserGroup>().HasData(userGroup1, userGroup2, userGroup3, userGroup4);
//
//         var room1 = new Room("General", new List<User> { user1, user2 }) { Group = group2 };
//         var room2 = new Room("Family Chat", new List<User> { user1, user2, user3, user4 }) { Group = group1 };
//         var room3 = Room.CreatePrivate(user1, user3);
//         var room4 = Room.CreatePrivate(user2, user4);
//
//         modelBuilder.Entity<Room>().HasData(room1, room2, room3, room4);
//
//         var userRoom1 = new UserRoom(user1, room1);
//         var userRoom2 = new UserRoom(user2, room1);
//         var userRoom3 = new UserRoom(user1, room2);
//         var userRoom4 = new UserRoom(user2, room2);
//         var userRoom5 = new UserRoom(user3, room2);
//         var userRoom6 = new UserRoom(user4, room2);
//         var userRoom7 = new UserRoom(user1, room3);
//         var userRoom8 = new UserRoom(user3, room3);
//         var userRoom9 = new UserRoom(user2, room4);
//         var userRoom10 = new UserRoom(user4, room4);
//
//         modelBuilder.Entity<UserRoom>().HasData(userRoom1, userRoom2, userRoom3, userRoom4, userRoom5, userRoom6, userRoom7, userRoom8, userRoom9, userRoom10);
//     }
// }