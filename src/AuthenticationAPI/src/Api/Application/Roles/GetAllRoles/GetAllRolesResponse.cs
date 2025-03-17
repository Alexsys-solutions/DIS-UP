namespace AuthenticationAPI.Api.Application.Roles.GetAllRoles;

/// <summary>
/// Represents a response containing details of a role.
/// </summary>
public class GetAllRolesResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the role.
    /// </summary>
    public string Id { get; set; } = default!;

    /// <summary>
    /// Gets or sets the name of the role.
    /// </summary>
    public string Name { get; set; } = default!;
    public string DisplayName { get; set; } = default!;

    public string RoleAr { get; set; } = default!;
}

