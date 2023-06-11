using Microsoft.EntityFrameworkCore;
using SyncLink.Application.Domain.Features;
using SyncLink.Application.Domain;
using SyncLink.Application.Domain.Features.TextPlotGame;
using SyncLink.Application.Domain.Groups;
using SyncLink.Application.Domain.Groups.Rooms;

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

    public DbSet<WordsChainGame> WordsChainGames { get; set; }

    public DbSet<WordsChainEntry> WordsChainEntries { get; set; }

    public DbSet<UserWordsChainGame> UsersToWordsChains { get; set; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}