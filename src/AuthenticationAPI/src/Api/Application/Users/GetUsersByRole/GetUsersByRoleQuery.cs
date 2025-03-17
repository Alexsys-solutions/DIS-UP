
using AuthenticationAPI.Api.Application.Common.Models;

namespace AuthenticationAPI.Api.Application.Users.GetUsersByRole;

/// <summary>
/// Represents a query for retrieving users by their role.
/// </summary>
public record GetUsersByRoleQuery : IRequest<List<UserResponse>>
{
    /// <summary>
    /// Gets the role for which users are queried.
    /// </summary>
    public string Role { get; init; }

    /// <summary>
    /// Gets the assigned Bch ID for filtering users.
    /// </summary>
    public int? AssignedBchId { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GetUsersByRoleQuery"/> class.
    /// </summary>
    /// <param name="role">The role for which users are queried.</param>
    /// <param name="assignedBchId">The assigned Bch ID for filtering users.</param>
    public GetUsersByRoleQuery(string role, int? assignedBchId)
    {
        Role = role;
        AssignedBchId = assignedBchId;
    }
}
