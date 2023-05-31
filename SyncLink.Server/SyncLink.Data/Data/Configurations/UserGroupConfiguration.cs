using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SyncLink.Application.Domain.Features;

namespace SyncLink.Infrastructure.Data.Configurations;

internal class UserToWordsChainGameConfiguration : IEntityTypeConfiguration<UserWordsChainGame>
{
    public void Configure(EntityTypeBuilder<UserWordsChainGame> builder)
    {
        builder
            .HasKey(
                nameof(UserWordsChainGame.UserId),
                nameof(UserWordsChainGame.GameId)
            );
    }
}