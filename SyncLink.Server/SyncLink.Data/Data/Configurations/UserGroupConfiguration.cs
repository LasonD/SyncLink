using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SyncLink.Application.Domain.Features;

namespace SyncLink.Infrastructure.Data.Configurations;

internal class UserToWordsChainGameConfiguration : IEntityTypeConfiguration<UserToWordsChainGame>
{
    public void Configure(EntityTypeBuilder<UserToWordsChainGame> builder)
    {
        builder
            .HasKey(
                nameof(UserToWordsChainGame.UserId),
                nameof(UserToWordsChainGame.GameId)
            );
    }
}