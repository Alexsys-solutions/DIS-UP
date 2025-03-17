using AuthenticationAPI.Domain.ApplicationUser.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthenticationAPI.Infrastructure.Configurations;

/// <summary>
/// Provides configuration for the Role entity.
/// </summary>
public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    /// <summary>
    /// Configures the Role entity.
    /// </summary>
    /// <param name="builder">The builder used to configure the Role entity.</param>
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        // Specifies the table name for the Role entity.
        builder.ToTable("AspNetRoles");
    }
}
