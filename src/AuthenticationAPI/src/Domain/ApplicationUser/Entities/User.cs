using Microsoft.AspNetCore.Identity;

namespace AuthenticationAPI.Domain.ApplicationUser.Entities;

/// <summary>
/// Represents a user in the authentication system, extending the IdentityUser class.
/// </summary>
public class User : IdentityUser<string>
{
    /// <summary>
    /// Gets or sets the first name of the user.
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name of the user.
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// Gets or sets the first name of the user in Arabic.
    /// </summary>
    public string FirstNameAr { get; set; } = default!;

    /// <summary>
    /// Gets or sets the last name of the user in Arabic.
    /// </summary>
    public string LastNameAr { get; set; } = default!;

    /// <summary>
    /// Gets or sets the description about the agent.
    /// </summary>
    public string? About { get; set; }

    /// <summary>
    /// Gets or sets the rating of the agent. Defaults to 0.
    /// </summary>
    public double Rating { get; set; } = 0;
    /// <summary>
    /// Gets or sets the collection of user roles.
    /// </summary>
    public ICollection<UserRole> UserRoles { get; set; } = [];

    /// <summary>
    /// Gets the role names associated with the user.
    /// </summary>
    /// <returns>A list of role names.</returns>
    public IList<string> GetRoleNames() => UserRoles.Count != 0 ? UserRoles.Select(ur => ur.Role.Name!).ToList() : [];

    /// <summary>
    /// Gets the list of BCH IDs the user is responsible for.
    /// </summary>
    /// <returns>A list of BCH IDs.</returns>
   /* public IList<short> GetResponsibleBchIds()
    {
        var responsibleBchIds = new List<short>();

        if (AssignedAuthorityStation?.AuthorityStationBchs != null)
        {
            responsibleBchIds.AddRange(AssignedAuthorityStation.AuthorityStationBchs.Select(bch => bch.BchId));
        }

        if (AssignedSecurityStation?.SecurityStationBchs != null)
        {
            responsibleBchIds.AddRange(AssignedSecurityStation.SecurityStationBchs.Select(bch => bch.BchId));
        }

        if (AssignedBchId != null)
            responsibleBchIds.Add(AssignedBchId.Value);

        return responsibleBchIds.Distinct().ToList();
    }

    /// <summary>
    /// Retrieves the user's assigned entity, which could be a BCH, an authority station, or a security station.
    /// </summary>
    /// <returns>The user's assigned entity or null if not assigned.</returns>
    public UserAssignmentBase? GetUserAssignmentEntity()
    {
        if (AssignedBch is not null) return AssignedBch;
        if (AssignedSecurityStation is not null) return AssignedSecurityStation;
        if (AssignedAuthorityStation is not null) return AssignedAuthorityStation;
        else return null;
    }*/
}
