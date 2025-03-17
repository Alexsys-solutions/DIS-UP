namespace AuthenticationAPI.Api.Application.Users.LockoutUser;

/// <summary>
/// Represents a query for locking out a user.
/// </summary>
public class LockoutUserQuery : IRequest<Unit>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LockoutUserQuery"/> class with the specified user ID.
    /// </summary>
    /// <param name="userId">The ID of the user to lock out.</param>
    public LockoutUserQuery(string userId)
    {
        UserId = userId;
    }

    /// <summary>
    /// Gets or sets the ID of the user to lock out.
    /// </summary>
    public string UserId { get; set; }
}