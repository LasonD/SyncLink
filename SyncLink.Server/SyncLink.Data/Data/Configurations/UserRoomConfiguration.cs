using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SyncLink.Application.Domain.Associations;

namespace SyncLink.Infrastructure.Data.Configurations;

internal class UserRoomConfiguration : IEntityTypeConfiguration<UserRoom>
{
    public void Configure(EntityTypeBuilder<UserRoom> builder)
    {
        builder
            .HasKey(
                nameof(UserRoom.UserId),
                nameof(UserRoom.RoomId)
            );
    }
}