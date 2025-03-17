using AuthenticationAPI.Domain.ApplicationUser.Entities;
using AuthenticationAPI.Domain.ApplicationUser.Interfaces;
using AuthenticationAPI.Infrastructure.Settings;
using Microsoft.IdentityModel.Tokens;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace AuthenticationAPI.Infrastructure.Providers;

/// <summary>
/// Helper class for generating JWT tokens for user authentication.
/// </summary>
public class TokenProvider : ITokenProvider
{
    private readonly JwtSettings _jwtSettings;
    private readonly OtpSettings _otpSettings;
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

    /// <summary>
    /// Initializes a new instance of the <see cref="TokenProvider"/> class.
    /// </summary>
    /// <param name="jwtSettings">The JWT settings.</param>
    /// <param name="otpSettings">The OTP settings.</param>
    public TokenProvider(JwtSettings jwtSettings, OtpSettings otpSettings)
    {
        _jwtSettings = jwtSettings ?? throw new ArgumentNullException(nameof(jwtSettings));
        _otpSettings = otpSettings ?? throw new ArgumentNullException(nameof(otpSettings));
        _jwtSecurityTokenHandler = new();
    }

    /// <inheritdoc />
    public string GenerateAccessToken(User user)
    {
        var claims = GetStandardUserClaims(user);
        AddUserAssignmentClaim(user, claims);
        AddRolesToClaims(user, claims);

        var token = GenerateToken(_jwtSettings.Issuer, _jwtSettings.Audience, _jwtSettings.SecretKey, _jwtSettings.ExpirationTokenInMinutes, claims);
        return _jwtSecurityTokenHandler.WriteToken(token);
    }

    /// <inheritdoc />
    public string GenerateOtpToken(OtpVerification otp)
    {
        var claims = new List<Claim>
        {
            new("phoneNumber", otp.PhoneNumber)
        };

        var token = GenerateToken(_otpSettings.Issuer, _otpSettings.Audience, _otpSettings.SecretKey, _otpSettings.ExpirationTimeInMinutes, claims);
        return _jwtSecurityTokenHandler.WriteToken(token);
    }

    private List<Claim> GetStandardUserClaims(User user)
    {
        return
        [
            new Claim("userName", user.UserName ?? string.Empty),
            new Claim("firstName", user.FirstName ?? string.Empty),
            new Claim("lastName", user.LastName ?? string.Empty),
            new Claim("userId", user.Id ?? string.Empty),
            new Claim("Roles", user.UserRoles != null && user.UserRoles.Count > 0
            ? string.Join(",", user.UserRoles.Select(role => role.Role.Name))
            : string.Empty)            
            //new Claim("assignmentBCH", user.AssignedBchId?.ToString() ?? string.Empty),
            //new Claim("responsibleBchIds", string.Join(",", user.GetResponsibleBchIds()))
        ];
    }

    /// <inheritdoc cref="ITokenProvider.GenerateAccessToken"/>
    string ITokenProvider.GenerateAccessToken(User user)
    {
        return GenerateAccessToken(user);
    }

    /// <inheritdoc cref="ITokenProvider.GenerateOtpToken"/>
    string ITokenProvider.GenerateOtpToken(OtpVerification otp)
    {
        return GenerateOtpToken(otp);
    }

    #region Helpers

    private static void AddUserAssignmentClaim(User user, List<Claim> claims)
    {
       
    }

    private static void AddRolesToClaims(User user, List<Claim> claims)
    {
        foreach (var role in user.GetRoleNames())
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
    }

    private static JwtSecurityToken GenerateToken(string issuer, string audience, string secretKey, int expirationTimeInMinutes, List<Claim> claims)
    {
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        return new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(expirationTimeInMinutes),
            signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
        );
    }

    #endregion
}