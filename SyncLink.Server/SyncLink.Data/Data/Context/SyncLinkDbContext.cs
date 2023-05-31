using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SyncLink.Application.Contracts.Data.RepositoryInterfaces;
using SyncLink.Application.Domain;
using SyncLink.Application.Domain.Associations;
using SyncLink.Application.Domain.Features;
using SyncLink.Application.Domain.Features.TextPlotGame;
using SyncLink.Infrastructure.Data.Models.Identity;
using System.Reflection;

namespace SyncLink.Infrastructure.Data.Context;

public class SyncLinkDbContext : IdentityDbContext<SyncLinkIdentityUser>, IAppDbContext
{
    public SyncLinkDbContext(DbContextOptions<SyncLinkDbContext> options)
        : base(options)
    {
    }

    public DbSet<Message> Messages { get; set; } = null!;

    public DbSet<Room> Rooms { get; set; } = null!;

    public DbSet<Group> Groups { get; set; } = null!;

    public DbSet<User> ApplicationUsers { get; set; } = null!;

    public DbSet<UserGroup> UsersToGroups { get; set; } = null!;

    public DbSet<UserRoom> UsersToRooms { get; set; } = null!;

    public DbSet<Whiteboard> Whiteboards { get; set; } = null!;

    public DbSet<TextPlotGame> TextPlotGames { get; set; } = null!;

    public DbSet<TextPlotEntry> TextPlotEntries { get; set; } = null!;

    public DbSet<TextPlotVote> TextPlotVotes { get; set; } = null!;

    public DbSet<WordsChainGame> WordsChainGames { get; set; } = null!;

    public DbSet<WordsChainEntry> WordsChainEntries { get; set; } = null!;

    public DbSet<UserWordsChainGame> UsersToWordsChains { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
