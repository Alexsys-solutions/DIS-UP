var builder = WebApplication.CreateBuilder(args);


// Configure Logging
builder.Logging
    .ClearProviders()
    .AddConfiguration(builder.Configuration);

if (builder.Environment.IsDevelopment())
{
    builder.Logging
        .AddConsole()
        .AddDebug();
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
    .AddSwaggerGen(c =>
    {
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
    })
    // Registers services to compress outputs.
    .AddResponseCompression()
    // Registers sensitive encryption services (e.g. to encrypt cookies).
    .AddDataProtection();

builder.Services
    // Registers MVC & Web API services.
    .AddMvcCore(
        options => options.Filters.Add<ApiExceptionFilter>()
    )
    .AddApiExplorer()
    .AddDataAnnotations()
    .AddAuthorization();

// [You can add your own application services here...]
builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration).AddCors(options =>
    {
        options.AddPolicy("AllowAll", builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
    });


var app = builder.Build();

// Configure Application middlewares pipeline
if (builder.Environment.IsDevelopment())
{
    // Uses development tools.
    app
        .UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI();
app
    .UseRouting()
    .UseCors("AllowAll") // Apply the CORS policy
    .UseResponseCompression()
    .UseAuthorization();

app.MapHealthChecks("/health");
app.MapControllers();

app.Run();
