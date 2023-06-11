using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SyncLink.Application.Domain.Groups;

namespace SyncLink.Infrastructure.Data.Configurations;

internal class UserGroupConfiguration : IEntityTypeConfiguration<UserGroup>
{
    public void Configure(EntityTypeBuilder<UserGroup> builder)
    {
        builder
            .HasKey(
                nameof(UserGroup.UserId),
                nameof(UserGroup.GroupId)
            );
    }
}