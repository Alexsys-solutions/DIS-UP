namespace AuthenticationAPI.Domain.ApplicationUser.Entities;

/// <summary>
/// Represents an anonymous user.
/// </summary>
public class OtpVerification
{
    /// <summary>
    /// Gets or sets the unique identifier of the user.
    /// </summary>
    public string Id { get; set; } = default!;

    /// <summary>
    /// Gets or sets the phone number associated with the user.
    /// </summary>
    public string PhoneNumber { get; set; } = default!;

    /// <summary>
    /// Gets or sets the verification code for the user.
    /// </summary>
    public string Code { get; set; } = default!;

    /// <summary>
    /// Gets or sets the timestamp when the user was created.
    /// </summary>
    public long CreatedAt { get; set; } = default!;

    /// <summary>
    /// Gets or sets the timestamp when the user's verification code expires.
    /// </summary>
    public long ExpiredOn { get; set; } = default!;

    /// <summary>
    /// Gets or sets the number of verification attempts made by the user.
    /// </summary>
    public int Attempts { get; set; } = 0;

    /// <summary>
    /// Gets or sets a value indicating whether the user is authenticated.
    /// </summary>
    public bool IsAuthenticated { get; set; }
}
