using AuthenticationAPI.Domain.ApplicationUser.Entities;
using AuthenticationAPI.Domain.ApplicationUser.Interfaces;
using AuthenticationAPI.Domain.Common.Interfaces;
using AuthenticationAPI.Infrastructure.Context;
using AuthenticationAPI.Infrastructure.Providers;
using AuthenticationAPI.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace AuthenticationAPI.Infrastructure;
/// <summary>
/// Represents a static class for dependency injection configuration.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Extension method to register infrastructure services required by the application.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/> instance representing the application's configuration.</param>
    /// <returns>The modified <see cref="IServiceCollection"/> instance with added services.</returns>
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Register JWT settings
        services.ConfigureSettings(configuration);

        // Register DbContexts
        services.ConfigureDbContexts(configuration);

        // Register Identity services
        services.ConfigureIdentity();

        // Register JWT authentication
        services.ConfigureJwtAuthentication(configuration);

        // Configure Identity options
        services.ConfigureIdentityOptions();

        // Register custom services
        services.ConfigureCustomServices();

        // Register cosmosdb services
        services.AddCosmosDbService(configuration);

        return services;
    }
    #region Helpers

    private static void ConfigureSettings(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = new JwtSettings();
        var otpSettings = new OtpSettings();

        configuration.Bind("JwtSettings", jwtSettings);
        configuration.Bind("OtpSettings", otpSettings);

        services
            .AddSingleton(jwtSettings)
            .AddSingleton(otpSettings);
    }

    private static void ConfigureDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("SqlPrimary")), ServiceLifetime.Scoped);
    }

    private static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
    }

    private static void ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind("JwtSettings", jwtSettings);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
            };
        });
    }

    private static void ConfigureIdentityOptions(this IServiceCollection services)
    {
        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 8;
        });
    }

    private static IServiceCollection AddCosmosDbService(this IServiceCollection services, IConfiguration configuration)

    {
        var databaseConfig = new DatabaseConfig();
        configuration.Bind(nameof(DatabaseConfig), databaseConfig);

        services.AddCosmosDbService(options =>
        {
            options.ConnectionString = configuration.GetConnectionString("CosmosPrimary")!;
            options.DatabaseId = databaseConfig.DatabaseId;
            options.ContainerId = databaseConfig.ContainerId;
        });
        return services;
    }

    private static void ConfigureCustomServices(this IServiceCollection services)
    {
        services
            .AddSingleton<ITokenProvider, TokenProvider>()
            .AddSingleton<IEmailService, EmailService>()
            .AddSingleton<IOtpVerificationRepository, OtpVerificationRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }

    #endregion
}
