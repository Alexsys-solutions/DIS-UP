using AuthenticationAPI.Domain.ApplicationUser.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthenticationAPI.Infrastructure.Configurations;

internal class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder
            .HasOne(u => u.Role)
            .WithMany(x => x.UserRoles)
            .HasForeignKey(u => u.RoleId);

        builder
            .HasOne(u => u.User)
            .WithMany(x => x.UserRoles)
            .HasForeignKey(u => u.UserId);
    }
}
