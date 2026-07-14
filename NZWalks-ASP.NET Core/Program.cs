using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Creates a WebApplicationBuilder.
// It initializes the application, loads configuration,
// sets up dependency injection (DI), logging, and other services.

// Registers MVC Controller services into the Dependency Injection (DI) container.
// Required if your application uses Controllers to handle HTTP requests.
builder.Services.AddControllers();

// Registers OpenAPI (Swagger) services.
// This generates API documentation that describes all available endpoints.
builder.Services.AddOpenApi();

// Builds the application.
// After this point, all registered services are ready to be used.
var app = builder.Build();

// Configure the HTTP request pipeline.

// Checks if the application is running in the Development environment.
if (app.Environment.IsDevelopment())
{
    // Exposes the OpenAPI specification endpoint.
    // This allows tools like Swagger UI to display API documentation.
    app.MapOpenApi();
}

// Redirects all HTTP requests to HTTPS automatically.
app.UseHttpsRedirection();

// Adds Authorization middleware.
// It checks whether the current user is authorized to access protected endpoints.
// (Authentication middleware should be added before this if authentication is used.)
app.UseAuthorization();

// Maps incoming HTTP requests to the appropriate Controller actions
// based on routing attributes such as [HttpGet], [HttpPost], etc.
app.MapControllers();

// Starts the application and begins listening for incoming HTTP requests.
app.Run();

