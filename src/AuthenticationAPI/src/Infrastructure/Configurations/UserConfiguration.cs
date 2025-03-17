using AuthenticationAPI.Domain.ApplicationUser.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthenticationAPI.Infrastructure.Configurations;

/// <summary>
/// Provides configuration for the User entity.
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    /// <summary>
    /// Configures the User entity.
    /// </summary>
    /// <param name="builder">The builder used to configure the User entity.</param>
    public void Configure(EntityTypeBuilder<User> builder)
    {
      /*  builder.HasOne(u => u.AssignedBch)
            .WithMany()
            .HasForeignKey(u => u.AssignedBchId);

        builder.HasOne(u => u.AssignedAuthorityStation)
            .WithMany()
            .HasForeignKey(u => u.AssignedAuthorityStationId);

        builder.HasOne(u => u.AssignedSecurityStation)
            .WithMany()
            .HasForeignKey(u => u.AssignedSecurityStationId);*/
    }
}
