using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SyncLink.Application.Domain.Groups;

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

        builder.Metadata
            .FindNavigation(nameof(Group.Rooms))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Metadata
            .FindNavigation(nameof(Group.UserGroups))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}