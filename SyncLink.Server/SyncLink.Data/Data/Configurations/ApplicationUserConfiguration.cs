using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SyncLink.Application.Domain;

namespace SyncLink.Infrastructure.Data.Configurations;

internal class ApplicationUserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasMany(x => x.UserGroups)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);

        builder
            .HasMany(x => x.UserRooms)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);

        builder
            .HasMany(x => x.Messages)
            .WithOne(x => x.Sender)
            .HasForeignKey(x => x.SenderId);
    }
}