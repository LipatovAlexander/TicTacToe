using Application;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public sealed class PlayerConfig : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.ToTable("Player");
        builder.HasKey(player => player.Id);

        builder
            .HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey("UserId");

        builder
            .Property(player => player.Mark)
            .HasConversion<string>();
    }
}