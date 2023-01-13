using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public sealed class GameConfig : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.ToTable("Game");
        builder.HasKey(game => game.Id);

        builder
            .HasOne(game => game.Host)
            .WithOne()
            .HasForeignKey<Game>("HostId");

        builder
            .HasOne(game => game.Opponent)
            .WithOne()
            .HasForeignKey<Game>("OpponentId");

        builder
            .Property(game => game.State)
            .HasConversion<string>();

        builder
            .Property(game => game.Board)
            .HasConversion<BoardConverter>();
    }
}