using AuthenticationAPI.Domain.ApplicationUser.Entities;

namespace AuthenticationAPI.Domain.ApplicationUser.Interfaces;

/// <summary>
/// Interface for providing JWT token generation functionality.
/// </summary>
public interface ITokenProvider
{
    /// <summary>
    /// Generates a JWT token for the specified user using the provided configuration.
    /// </summary>
    /// <param name="user">The user for whom the token is generated.</param>
    /// <returns>The generated JWT token as a string.</returns>
    string GenerateAccessToken(User user);

    /// <summary>
    /// Generates an OTP (One-Time Password) token for the specified OTP verification.
    /// </summary>
    /// <param name="otp">The OTP verification information.</param>
    /// <returns>The generated OTP token as a string.</returns>
    string GenerateOtpToken(OtpVerification otp);
}
