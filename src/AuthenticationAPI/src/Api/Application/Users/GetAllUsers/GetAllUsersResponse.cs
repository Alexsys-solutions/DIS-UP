using AuthenticationAPI.Api.Application.Common.Models;
using AuthenticationAPI.Domain.ApplicationUser.Entities;

namespace AuthenticationAPI.Api.Application.Users.GetAllUsers;

/// <summary>
/// Represents a response containing details of all users.
/// </summary>
public class GetAllUsersResponse
{
    /// <summary>
    /// Gets or sets the user details.
    /// </summary>
    public UserResponse User { get; set; } = default!;

    /// <summary>
    /// Gets or sets the roles associated with the user.
    /// </summary>
    public IEnumerable<Role> Roles { get; set; } = default!;
}
