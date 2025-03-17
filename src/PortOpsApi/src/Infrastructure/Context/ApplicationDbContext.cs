using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sonasid.DisUp.PortOps.Domain.Users.Entities;

namespace Sonasid.DisUp.PortOps.Infrastructure.Context;

/// <summary>
/// Represents the application database context, extending IdentityDbContext for Project management.
/// </summary>
public class ApplicationDbContext : IdentityDbContext<User>
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<User>(b =>
        {
            b.ToTable("AspNetUsers");
        });
       
    }
    /// <summary>
    /// Constructor for ApplicationDbContext.
    /// </summary>
    /// <param name="options">The DbContext options.</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
}