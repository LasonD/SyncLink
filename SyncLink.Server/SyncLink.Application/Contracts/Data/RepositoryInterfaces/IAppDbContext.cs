﻿using Microsoft.EntityFrameworkCore;
using SyncLink.Application.Domain.Associations;
using SyncLink.Application.Domain.Features;
using SyncLink.Application.Domain;

namespace SyncLink.Application.Contracts.Data.RepositoryInterfaces;

public interface IAppDbContext
{
    public DbSet<Message> Messages { get; set; }

    public DbSet<Room> Rooms { get; set; }

    public DbSet<Group> Groups { get; set; }

    public DbSet<User> ApplicationUsers { get; set; }

    public DbSet<UserGroup> UsersToGroups { get; set; }

    public DbSet<UserRoom> UsersToRooms { get; set; }

    public DbSet<Whiteboard> Whiteboards { get; set; }

    public DbSet<TextPlotGame> TextPlotGames { get; set; }

    public DbSet<TextPlotEntry> TextPlotEntries { get; set; }

    public DbSet<TextPlotVote> TextPlotVotes { get; set; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}