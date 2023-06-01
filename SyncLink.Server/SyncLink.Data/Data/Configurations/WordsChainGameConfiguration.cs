using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SyncLink.Application.Domain.Features;

namespace SyncLink.Infrastructure.Data.Configurations;

internal class WordsChainGameConfiguration : IEntityTypeConfiguration<WordsChainGame>, IEntityTypeConfiguration<WordsChainEntry>,
    IEntityTypeConfiguration<UserWordsChainGame>
{
    public void Configure(EntityTypeBuilder<WordsChainGame> builder)
    {
        builder.HasMany(x => x.Entries)
            .WithOne(x => x.Game)
            .HasForeignKey(x => x.GameId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }

    public void Configure(EntityTypeBuilder<WordsChainEntry> builder)
    {
        builder.HasOne(x => x.Participant)
            .WithMany()
            .HasForeignKey(x => new { x.UserId, x.GameId })
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();
    }

    public void Configure(EntityTypeBuilder<UserWordsChainGame> builder)
    {
        builder
            .HasKey(
                nameof(UserWordsChainGame.UserId),
                nameof(UserWordsChainGame.GameId)
            );
    }
}