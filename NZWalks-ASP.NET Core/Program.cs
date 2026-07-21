using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NZWalks_ASP.NET_Core.Data;
using NZWalks_ASP.NET_Core.Repositories;
using Scalar.AspNetCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Creates a WebApplicationBuilder.
// It initializes the application, loads configuration,
// sets up dependency injection (DI), logging, and other services.

// Registers MVC Controller services into the Dependency Injection (DI) container.
// Required if your application uses Controllers to handle HTTP requests.
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();

// Configure Swashbuckle for JWT Bearer Token
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme,
        new OpenApiSecurityScheme
        {
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = JwtBearerDefaults.AuthenticationScheme,
            BearerFormat = "JWT",
            Description = "Enter JWT Bearer token only."
        });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            Array.Empty<string>()
        }
    });
});

// PostgreSQL Connection
builder.Services.AddDbContext<NZWalksDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("NZWalksConnectionString")));

builder.Services.AddDbContext<NZWalksAuthDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("NZWalksAuthConnectionString")));

builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("NZWalks")
    .AddEntityFrameworkStores<NZWalksAuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<ImageRepository, LocalImageRepository>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

});

// Removed AddOpenApi here because it was configured above.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
            )
        };
    });

// Builds the application.
// After this point, all registered services are ready to be used.
var app = builder.Build();

// Configure the HTTP request pipeline.

// Checks if the application is running in the Development environment.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.MapScalarApiReference(options =>
    {
        options.WithOpenApiRoutePattern("/swagger/v1/swagger.json");
    });
}

// Redirects all HTTP requests to HTTPS automatically.
app.UseHttpsRedirection();


app.UseAuthentication();

// Adds Authorization middleware.
// It checks whether the current user is authorized to access protected endpoints.
// (Authentication middleware should be added before this if authentication is used.)
app.UseAuthorization();

// Maps incoming HTTP requests to the appropriate Controller actions
// based on routing attributes such as [HttpGet], [HttpPost], etc.
app.MapControllers();

// Starts the application and begins listening for incoming HTTP requests.
app.Run();

