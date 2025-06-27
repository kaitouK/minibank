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
builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddScoped<IUserService, UserService>();// Register the UserService as IUserService
builder.Services.AddScoped<JwtTokenService>();// Register the JwtTokenService for dependency injection
builder.Services.AddScoped<IAccountService, AccountService>(); // Register the AccountService as IAccountService
builder.Services.AddScoped<ITransactionService, TransactionService>(); // Register the TransactionService as ITransaction
builder.Services.AddCorsServices(); // Add CORS services to allow the React app to access the API
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers(); // Add services for controllers
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnet/core/swashbuckle
builder.Services.AddSwaggerWithJwt(); // Add Swagger services for API documentation, include AddSwaggerGen and OpenApiInfo
builder.Services.AddHttpContextAccessor(); // Add HttpContextAccessor to access the current HTTP context
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>(); // Add custom exception handling middleware

app.UseHttpsRedirection();
app.UseRouting(); // Enable routing for the application
//middleware pipeline configuration
// Configure the HTTP request pipeline.
app.UseMiddleware<SecurityHeaders>(); // Add security headers middleware

// Configure the HTTP request pipeline.
// Use https://localhost:5146/swagger/index.html to access the Swagger UI
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowReactApp");// Enable CORS for the React app
app.UseAuthentication();
/* // Uncomment this middleware to log authentication details
app.Use(async (context, next) =>
{
    Console.WriteLine($"Auth Middleware - User Authenticated: {context.User?.Identity?.IsAuthenticated}");
    if (context.User?.Claims != null)
    {
        foreach (var claim in context.User.Claims)
        {
            Console.WriteLine($"  Claim: {claim.Type} = {claim.Value}");
        }
    }
    await next(context);
});*/

app.UseAuthorization();
app.Use(async (context, next) =>
{
    var user = context.User;
    var isAuthenticated = user?.Identity?.IsAuthenticated ?? false;
    // 在這裡查看 user 和 isAuthenticated 的值
    await next(context);
});

app.MapControllers();// map controllers to the request pipeline, important for API endpoints to be accessible

app.UseSwagger();
app.UseSwaggerUI();

app.Run();


