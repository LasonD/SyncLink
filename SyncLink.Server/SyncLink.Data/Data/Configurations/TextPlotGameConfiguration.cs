using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SyncLink.Application.Domain.Features.TextPlotGame;

namespace SyncLink.Infrastructure.Data.Configurations;

internal class TextPlotGameConfiguration : IEntityTypeConfiguration<TextPlotGame>
{
    public void Configure(EntityTypeBuilder<TextPlotGame> builder)
    {
        builder.HasOne(x => x.Group)
            .WithMany()
            .HasForeignKey(x => x.GroupId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasMany(x => x.Entries)
            .WithOne(x => x.Game)
            .HasForeignKey(x => x.GameId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}

internal class TextPlotEntryConfiguration : IEntityTypeConfiguration<TextPlotEntry>
{
    public void Configure(EntityTypeBuilder<TextPlotEntry> builder)
    {
        builder.HasMany(x => x.Votes)
            .WithOne(x => x.Entry)
            .HasForeignKey(x => x.EntryId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);
    }
}

internal class TextPlotVoteConfiguration : IEntityTypeConfiguration<TextPlotVote>
{
    public void Configure(EntityTypeBuilder<TextPlotVote> builder)
    {
        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);

        builder.HasOne(x => x.Entry)
            .WithMany()
            .HasForeignKey(x => x.EntryId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}