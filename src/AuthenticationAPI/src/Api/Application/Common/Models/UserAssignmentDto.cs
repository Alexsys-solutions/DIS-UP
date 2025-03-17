namespace AuthenticationAPI.Api.Application.Common.Models;

/// <summary>
/// Represents a data transfer object (DTO) for user assignments.
/// </summary>
public class UserAssignmentDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the user assignment.
    /// </summary>
    public short Id { get; set; }

    /// <summary>
    /// Gets or sets the Arabic name of the user assignment.
    /// </summary>
    public string NameAr { get; set; } = default!;

    /// <summary>
    /// Gets or sets the French name of the user assignment.
    /// </summary>
    public string NameFr { get; set; } = default!;
}
