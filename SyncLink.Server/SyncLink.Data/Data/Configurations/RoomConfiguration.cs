using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SyncLink.Application.Domain;

namespace SyncLink.Infrastructure.Data.Configurations;

internal class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder
            .HasMany(x => x.RoomMembers)
            .WithOne(x => x.Room)
            .HasForeignKey(x => x.RoomId);

        builder
            .HasMany(x => x.Messages)
            .WithOne(x => x.Room)
            .HasForeignKey(x => x.RoomId);

        builder
            .HasOne(x => x.Group)
            .WithMany(x => x.Rooms)
            .HasForeignKey(x => x.GroupId);
    }
}