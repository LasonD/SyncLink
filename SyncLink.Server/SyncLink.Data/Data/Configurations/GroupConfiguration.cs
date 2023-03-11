using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SyncLink.Application.Domain;

namespace SyncLink.Infrastructure.Data.Configurations;

internal class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder
            .HasMany(x => x.UserGroups)
            .WithOne(x => x.Group)
            .HasForeignKey(x => x.GroupId);

        builder
            .HasMany(x => x.Rooms)
            .WithOne(x => x.Group)
            .HasForeignKey(x => x.GroupId);
    }
}