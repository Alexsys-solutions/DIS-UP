namespace AuthenticationAPI.Infrastructure.Settings;

/// <summary>
/// Configuration settings for JWT tokens.
/// </summary>
public class JwtSettings
{
    /// <summary>
    /// Gets or sets the issuer of the token.
    /// </summary>
    public string Issuer { get; set; } = default!;

    /// <summary>
    /// Gets or sets the audience for the token.
    /// </summary>
    public string Audience { get; set; } = default!;

    /// <summary>
    /// Gets or sets the secret key used to sign the token.
    /// </summary>
    public string SecretKey { get; set; } = default!;

    /// <summary>
    /// Gets or sets the expiration time for the access token in hours.
    /// </summary>
    public int ExpirationTokenInHours { get; set; }

    /// <summary>
    /// Gets the expiration time for the access token in minutes.
    /// </summary>
    public int ExpirationTokenInMinutes => ExpirationTokenInHours * 60;
}
