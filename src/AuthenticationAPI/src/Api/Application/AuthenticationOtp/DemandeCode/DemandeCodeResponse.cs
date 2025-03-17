namespace AuthenticationAPI.Api.Application.AuthenticationOtp.DemandeCode;

/// <summary>
/// Represents the response containing the verification code.
/// </summary>
public class DemandeCodeResponse
{
    /// <summary>
    /// Gets or sets the verification code.
    /// </summary>
    public string Code { get; set; } = default!;
}
