namespace AuthenticationAPI.Api.Application.Users.Register;
/// <summary>
/// Represents a command for user registration.
/// </summary>
public record RegisterCommand : IRequest<string>
{
    public Guid Id { get; set; }
    /// <summary>
    /// Gets or initializes the username for the registration.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// Gets or initializes the password for the registration.
    /// </summary>
    public required string Password { get; init; }

    /// <summary>
    /// Gets or initializes the first name for the registration.
    /// </summary>
    public required string FirstName { get; init; }

    /// <summary>
    /// Gets or initializes the last name for the registration.
    /// </summary>
    public required string LastName { get; init; }

    /// <summary>
    /// Gets or initializes the Arabic first name for the registration.
    /// </summary>
    public string FirstNameAr { get; init; } = default!;

    /// <summary>
    /// Gets or initializes the Arabic last name for the registration.
    /// </summary>
    public string LastNameAr { get; init; } = default!;

    /// <summary>
    /// Gets or initializes the assigned Bch ID for the registration.
    /// </summary>
    //public int? AssignedBchId { get; init; }

    /// <summary>
    /// Gets or initializes the phone number for the registration.
    /// </summary>
    public required string PhoneNumber { get; init; }

    /// <summary>
    /// Gets or initializes the Email for the registration.
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

    /// <summary>
    /// Gets or initializes the role for the registration.
    /// </summary>
    public List<string> Roles { get; init; } = default!;

}
