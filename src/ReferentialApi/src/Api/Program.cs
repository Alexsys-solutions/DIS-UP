using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Configure Configuration
// Because appSettings, appSettings.<Environment>, EnvironmentVariables, command line configuration providers
// have been added by-design with asp.net core default host builder
// only add your additional configuration providers here
builder.Configuration
  // Add Kubernetes Config Map from path stored in `KUBERNETES_CONFIGMAP_PATH` environment variable.
  .AddKubernetesConfigMap()
  // Add Kubernetes Secrets from path stored in `KUBERNETES_SECRETS_PATH`environment variable.
  .AddKubernetesSecrets();

// Configure Logging
builder.Logging
    .ClearProviders()
    .AddConfiguration(builder.Configuration);

if (builder.Environment.IsLocal())
{
    builder.Logging
        .AddConsole()
        .AddDebug();
}
else
{
    builder.Logging
        .AddMonithorLog4Net();
}

// Configure Services
builder.Services
  .AddHealthChecks()
  .AddCheck("Default", () => HealthCheckResult.Healthy("OK"))
  // [You can add more checks here...]
  ;

builder.Services
    // Registers the Swagger generator, defining one Swagger document.
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddMetrics(options =>
    {
        options.ExposeHistogram(false);
    })
    // Registers services to compress outputs.
    .AddResponseCompression()
    // Registers sensitive encryption services (e.g. to encrypt cookies).
    .AddDataProtection();

builder.Services
    // Registers MVC & Web API services.
    .AddMvcCore()
    .AddApiExplorer()
    .AddDataAnnotations()
    .AddAuthorization();

// [You can add your own application services here...]

var app = builder.Build();

// Configure Application middlewares pipeline
if (builder.Environment.IsLocal())
{
    // Uses development tools.
    app
        .UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionMonithorLogging();
}

if (app.Environment.IsLocal())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app
    .UseRouting()
    .UseResponseCompression()
    .UseMetrics()
    .UseAuthorization();

app.MapHealthChecks("/health");
app.MapControllers();

app.Run();