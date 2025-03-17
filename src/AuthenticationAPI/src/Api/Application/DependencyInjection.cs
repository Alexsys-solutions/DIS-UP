using AuthenticationAPI.Api.Application.Common.Behaviours;

namespace AuthenticationAPI.Api.Application;

/// <summary>
/// A static class containing extension methods to configure dependency injection for application services.
/// </summary>
[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    /// <summary>
    /// Adds application services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to which application services are added.</param>
    /// <returns>The modified <see cref="IServiceCollection"/> with added application services.</returns>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Adds Mapster services using the current executing assembly
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        // Adds validators from the current executing assembly to the service collection
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // Adds MediatR services to the service collection
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        });

        return services;
    }
}
