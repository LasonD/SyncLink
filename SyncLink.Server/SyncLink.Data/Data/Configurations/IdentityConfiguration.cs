using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SyncLink.Infrastructure.Data.Models.Identity;

namespace SyncLink.Infrastructure.Data.Configurations;

internal class IdentityConfiguration : IEntityTypeConfiguration<SyncLinkIdentityUser>
{
    public void Configure(EntityTypeBuilder<SyncLinkIdentityUser> builder)
    {
        builder
            .HasOne(x => x.ApplicationUser)
            .WithOne()
            .HasForeignKey(typeof(SyncLinkIdentityUser), nameof(SyncLinkIdentityUser.ApplicationUserId));
    }
}