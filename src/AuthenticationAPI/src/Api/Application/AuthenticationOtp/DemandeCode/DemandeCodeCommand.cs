namespace AuthenticationAPI.Api.Application.AuthenticationOtp.DemandeCode;

/// <summary>
/// Command for requesting a verification code.
/// </summary>
public class DemandeCodeCommand : IRequest<Unit>
{
    /// <summary>
    /// Gets or sets the phone number for which the verification code is requested.
    /// </summary>
    public string PhoneNumber { get; set; } = default!;
}
