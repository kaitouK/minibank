using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;

namespace MyMiniBank.Api.Middlewares
{
    public static class ExceptionMiddlewareExtensions
    {
        [Obsolete("This method is obsolete and will be removed in future versions. Use the new ConfigureExceptionHandler method instead.")]
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {

                        var request = context.Request;
                        logger.LogError(contextFeature.Error,
                            "An unhandled exception occurred while processing the request. Method: {Method}, Path: {Path}, QueryString: {QueryString}, Exception: {ErrorMessage}",
                            request.Method,
                            request.Path,
                            request.QueryString,
                            contextFeature.Error.Message);
                        // Create a generic error response
                        var errorResponse = new
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "An unexpected error occurred. Please try again later."
                            // 可根據開發階段決定是否加入 contextFeature.Error.Message
                        };
                        var jsonResponse = JsonSerializer.Serialize(errorResponse);

                        await context.Response.WriteAsync(jsonResponse);
                    }
                });
            });
        }
    }
}
