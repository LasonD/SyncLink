using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SyncLink.Application.Domain.Features;

namespace SyncLink.Infrastructure.Data.Configurations;

internal class WhiteboardConfiguration : IEntityTypeConfiguration<Whiteboard>, IEntityTypeConfiguration<WhiteboardElement>
{
    public void Configure(EntityTypeBuilder<Whiteboard> builder)
    {
        builder.HasOne(x => x.Group)
            .WithMany()
            .HasForeignKey(x => x.GroupId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);

        builder.HasOne(x => x.Owner)
            .WithMany()
            .HasForeignKey(x => x.OwnerId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);
    }

    public void Configure(EntityTypeBuilder<WhiteboardElement> builder)
    {
        builder.HasOne(x => x.Author)
            .WithMany()
            .HasForeignKey(x => x.AuthorId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);

        builder.HasOne(x => x.Whiteboard)
            .WithMany(x => x.WhiteboardElements)
            .HasForeignKey(x => x.WhiteboardId)
            .OnDelete(DeleteBehavior.SetNull)
            .IsRequired(false);

        builder.OwnsOne(x => x.Options);
    }
}