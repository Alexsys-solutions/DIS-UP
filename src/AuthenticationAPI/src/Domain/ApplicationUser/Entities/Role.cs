using Microsoft.AspNetCore.Identity;

namespace AuthenticationAPI.Domain.ApplicationUser.Entities;

/// <summary>
/// Represents a role in the authentication system, extending the IdentityRole class.
/// </summary>
public class Role : IdentityRole<string>
{
    /// <summary>
    /// Gets or sets the role name in Arabic.
    /// </summary>
    public string? RoleAr { get; set; } = default!;

    /// <summary>
    /// Gets or sets the display name of the role.
    /// </summary>
    public string DisplayName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the collection of user roles associated with this role.
    /// </summary>
    public ICollection<UserRole> UserRoles { get; set; } = [];
}
