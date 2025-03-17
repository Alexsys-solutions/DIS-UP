using Microsoft.AspNetCore.Identity;

namespace AuthenticationAPI.Domain.ApplicationUser.Entities;

/// <summary>
/// Represents a user role in the authentication system, extending the IdentityUserRole class.
/// </summary>
public class UserRole : IdentityUserRole<string>
{
    /// <summary>
    /// Gets or sets the user associated with the role.
    /// </summary>
    public User User { get; set; } = default!;

    /// <summary>
    /// Gets or sets the role associated with the user.
    /// </summary>
    public Role Role { get; set; } = default!;
}

