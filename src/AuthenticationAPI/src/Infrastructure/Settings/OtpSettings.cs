namespace AuthenticationAPI.Infrastructure.Settings;

/// <summary>
/// Represents the configuration settings for OTP (One-Time Passwords).
/// </summary>
public record OtpSettings
{
    /// <summary>
    /// Gets or sets the expiration time for the OTP code (in seconds).
    /// </summary>
    public int ExpiredCode { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of allowed attempts for entering the OTP code.
    /// </summary>
    public int AllowedAttemps { get; set; }

    /// <summary>
    /// Gets or sets the secret key used for generating and validating OTP codes.
    /// </summary>
    public string SecretKey { get; set; } = default!;

    /// <summary>
    /// Gets or sets the issuer of the OTP codes.
    /// </summary>
    public string Issuer { get; set; } = default!;

    /// <summary>
    /// Gets or sets the audience of the OTP codes.
    /// </summary>
    public string Audience { get; set; } = default!;

    /// <summary>
    /// Gets or sets the expiration time for the OTP codes (in minutes).
    /// </summary>
    public int ExpirationTimeInMinutes { get; set; }
}

