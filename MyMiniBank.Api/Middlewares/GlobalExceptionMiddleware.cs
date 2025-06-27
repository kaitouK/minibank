using System.Net;
using System.Text.Json;
namespace MyMiniBank.Api.Middlewares
{
    /// <summary>
    /// Middleware to handle global exceptions in the application.
    /// This middleware catches exceptions thrown during the request processing pipeline
    /// </summary>
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var request = context.Request;
                // Log the exception details
                _logger.LogError(ex, "An unhandled exception occurred. \n Method: {Method}, \n Path: {Path}, \n QueryString: {QueryString}, \n Exception: {ErrorMessage}",
                    request.Method,
                    request.Path,
                    request.QueryString,
                    ex.Message);
                // Create a generic error response
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                // Prepare the response object
                // Optionally include detailed error information in development mode
                var response = new
                {
                    StatusCode = context.Response.StatusCode,
                    Message = "An unexpected error occurred. Please try again later.",
                    Detailed = _env.IsDevelopment() ? ex.Message : null // Show detailed error in development mode only
                };
                // Serialize the response to JSON
                var jsonResponse = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(jsonResponse);
            }
        }

    }
}