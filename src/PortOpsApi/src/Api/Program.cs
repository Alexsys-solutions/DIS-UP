using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Sonasid.DisUp.PortOps.Api.Application;
using Sonasid.DisUp.PortOps.Api.Filters;
using Sonasid.DisUp.PortOps.Infrastructure;
using System.Text;

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
  .AddCheck("Default", () => HealthCheckResult.Healthy("OKe"))
  // [You can add more checks here...]
  ;

builder.Services
    // Registers the Swagger generator, defining one Swagger document.
    .AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "JWTToken_Auth_API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer Auth Token\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
})    
    // Registers services to compress outputs.
    .AddResponseCompression()
    // Registers sensitive encryption services (e.g. to encrypt cookies).
    .AddDataProtection();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]))
        };
    });

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
    .UseStaticFiles()
    .UseCors("AllowAll") // Apply the CORS policy
    .UseResponseCompression()
    .UseAuthentication()
    .UseAuthorization();

app.MapHealthChecks("/health");
app.MapControllers();

app.Run();
