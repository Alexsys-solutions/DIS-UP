using Als.Foundation.Web.Authentication;
namespace Sonasid.DisUp.PortOps.Api.Extensions;

/// <summary>
/// Provides extension methods for configuring JWT Bearer Authentication.
/// </summary>
public static class JwtAuthenticationExtensions
{
    /// <summary>
    /// Configures JWT Bearer Authentication.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddJwtBearerAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .UseJwtBearerAuthentication(options =>
            {
                options.ValidIssuer = configuration["Jwt:Issuer"]!;
                options.ValidAudience = configuration["Jwt:Audience"]!;
                options.SecretKey = configuration["Jwt:SecretKey"]!;
            });

        return services;
    }
}