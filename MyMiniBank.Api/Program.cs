using Microsoft.EntityFrameworkCore;
using MyMiniBank.Api.Models.DataBaseContext;
using MyMiniBank.Api.Services;
using MyMiniBank.Api.Services.Interface;
using MyMiniBank.Api.Middlewares;
using MyMiniBank.Api.Models.Config;
using MyMiniBank.Api.Extensions;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Configuration.AddJsonFile("config/secrets.json", optional: true, reloadOnChange: true); // Load secrets from JSON file, optional means it won't throw an error if the file is not found
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));// Configure JWT settings from appsettings.json
//builder.Services.AddSingleton<JwtSettings>(); // Register JwtSettings as a singleton service
//var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();// Load JWT settings from configuration

builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddScoped<IUserService, UserService>();// Register the UserService as IUserService
builder.Services.AddScoped<JwtTokenService>();// Register the JwtTokenService for dependency injection
builder.Services.AddCorsServices(); // Add CORS services to allow the React app to access the API
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers(); // Add services for controllers
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnet/core/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>(); // Add custom exception handling middleware

//middleware pipeline configuration
// Configure the HTTP request pipeline.
app.UseMiddleware<SecurityHeaders>(); // Add security headers middleware

app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.
// Use https://localhost:5146/swagger/index.html to access the Swagger UI
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.MapControllers();// map controllers to the request pipeline, important for API endpoints to be accessible
app.UseHttpsRedirection();

app.UseCors("AllowReactApp");// Enable CORS for the React app
app.UseAuthentication();
app.UseAuthorization();

app.Run();


