using AuthenticationAPI.Domain.ApplicationUser.Entities;
using AuthenticationAPI.Infrastructure.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationAPI.Infrastructure.Context;

/// <summary>
/// Represents the application database context, extending IdentityDbContext for User management.
/// </summary>
public class ApplicationDbContext : IdentityDbContext<User, Role, string, IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new UserRoleConfiguration());
    }
    /// <summary>
    /// Constructor for ApplicationDbContext.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
}