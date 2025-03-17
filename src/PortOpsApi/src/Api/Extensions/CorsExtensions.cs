namespace Sonasid.DisUp.PortOps.Api.Extensions;

/// <summary>
/// Contains extension methods related to CORS configuration.
/// </summary>
[ExcludeFromCodeCoverage]
internal static class CorsExtensions
{
    /// <summary>
    /// Configures a custom CORS policy based on allowed origins from IConfiguration.
    /// </summary>
    /// <param name="services">The IServiceCollection to which CORS services will be added.</param>
    /// <param name="configuration">The IConfiguration containing allowed origins.</param>
    public static IServiceCollection AddCustomCorsPolicy(this IServiceCollection services, IConfiguration configuration)
    {
        var allowedOrigins = configuration.GetSection("AllowedOrigins").Get<string[]>();

        if (allowedOrigins != null && allowedOrigins.Length != 0)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder.WithOrigins(allowedOrigins)
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });
        }
        return services;
    }

    /// <summary>
    /// Applies the configured custom CORS policy to the application.
    /// </summary>
    /// <param name="app">The IApplicationBuilder representing the application.</param>
    /// <param name="configuration">The IConfiguration containing allowed origins.</param>
    public static void UseCustomCors(this IApplicationBuilder app, IConfiguration configuration)
    {
        if (configuration.GetSection("AllowedOrigins").Exists())
        {
            app.UseCors("AllowSpecificOrigin");
        }
    }
}
