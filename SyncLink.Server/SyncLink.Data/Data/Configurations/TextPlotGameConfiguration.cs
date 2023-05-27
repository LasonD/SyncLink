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
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);

        builder.OwnsMany(x => x.Entries, e =>
        {
            e.WithOwner();
            e.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            e.HasOne(x => x.Game)
                .WithMany()
                .HasForeignKey(x => x.GameId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            e.OwnsMany(x => x.Votes, v =>
            {
                v.WithOwner();
                v.HasOne(x => x.User)
                    .WithMany()
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .IsRequired(false);

                v.HasOne(x => x.Entry)
                    .WithMany()
                    .HasForeignKey(x => x.EntryId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });
        });
    }
}