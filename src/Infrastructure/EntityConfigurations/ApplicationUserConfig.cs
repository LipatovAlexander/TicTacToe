using Application;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public sealed class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("User");
        builder.HasKey(user => user.Id);
        builder.HasOne<User>().WithOne().HasForeignKey<User>(u => u.Id);
        builder.Property(u => u.UserName).HasColumnName("UserName");
    }
}