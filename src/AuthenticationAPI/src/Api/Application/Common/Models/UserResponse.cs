namespace AuthenticationAPI.Api.Application.Common.Models;

/// <summary>
/// Represents the response model for user-related operations.
/// </summary>
public record UserResponse
{
    /// <summary>
    /// Gets or sets the user's Id.
    /// </summary>
    public required string Id { get; init; }

    /// <summary>
    /// Gets or sets the user's username.
    /// </summary>
    public required string UserName { get; init; }

    /// <summary>
    /// Gets or sets the user's first name.
    /// </summary>
    public required string FirstName { get; init; }

    /// <summary>
    /// Gets or sets the user's last name.
    /// </summary>
    public required string LastName { get; init; }

    /// <summary>
    /// Gets or sets the user's first name in Arabic.
    /// </summary>
    public string FirstNameAr { get; init; } = default!;

    /// <summary>
    /// Gets or sets the user's last name in Arabic.
    /// </summary>
    public string LastNameAr { get; init; } = default!;

    /// <summary>
    /// Gets or sets the user's phone number.
    /// </summary>
    public required string PhoneNumber { get; init; }

    /// <summary>
    /// Gets or sets the user's Email.
    /// </summary>
    public required string Email { get; init; }

    /// <summary>
    /// Gets or sets the description about the agent.
    /// </summary>
    public string? About { get; set; }

    /// <summary>
    /// Gets or sets the rating of the agent. Defaults to 0.
    /// </summary>
    public double Rating { get; set; } = 0;

    ///<summary>
    ///Gets or sets the date and time when the user's lockout period ends, if any.
    ///</summary>
    public DateTimeOffset? LockoutEnd { get; set; }
}

