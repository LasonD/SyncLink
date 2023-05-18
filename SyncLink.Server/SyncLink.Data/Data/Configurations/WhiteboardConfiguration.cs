using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SyncLink.Application.Domain.Features;

namespace SyncLink.Infrastructure.Data.Configurations;

internal class WhiteboardConfiguration : IEntityTypeConfiguration<Whiteboard>
{
    public void Configure(EntityTypeBuilder<Whiteboard> builder)
    {
        builder.OwnsMany(x => x.WhiteboardElements, e =>
        {
            e.WithOwner();
            e.OwnsOne(x => x.Options);
        });
    }
}