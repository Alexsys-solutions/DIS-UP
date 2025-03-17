using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Sonasid.DisUp.PortOps.Api.Extensions;

/// <summary>
/// A class containing extension methods related to health checks.
/// </summary>
internal static class HealthCheckExtensions
{
    /// <summary>
    /// Configures custom health checks for the application.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> instance.</param>
    [ExcludeFromCodeCoverage]
    public static void UseCustomHealthChecks(this IApplicationBuilder app)
    {
        var managementPort = Environment.GetEnvironmentVariable("MANAGEMENT_HTTP_PORT");

        if (int.TryParse(managementPort, out var portNumber))
        {
            app.UseHealthChecks("/health", portNumber);
            app.UseHealthChecks("/ping", portNumber, new HealthCheckOptions()
            {
                Predicate = (_) => false
            });
        }
        else
        {
            app.UseHealthChecks("/health");
            app.UseHealthChecks("/ping", new HealthCheckOptions()
            {
                Predicate = (_) => false
            });
        }
    }
}
