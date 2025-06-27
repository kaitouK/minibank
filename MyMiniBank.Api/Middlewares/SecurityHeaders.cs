
namespace MyMiniBank.Api.Middlewares
{
    /// <summary>
    /// Middleware to add security headers to HTTP responses.
    /// This middleware is designed to enhance the security of the API by adding various HTTP headers that help protect against common web vulnerabilities.
    /// It includes headers for content security policy, XSS protection, frame options, and more.
    /// </summary>
    public class SecurityHeaders
    {
        private readonly RequestDelegate _next;

        public SecurityHeaders(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Add security headers to the response
            context.Response.Headers["Content-Security-Policy"] = "frame-ancestors 'self' https://localhost:3000"; // Allow the React app to embed this API, avoid clickjacking attacks
            context.Response.Headers["X-Content-Type-Options"] = "nosniff"; // Prevent MIME type sniffing, which can lead to security vulnerabilities
            context.Response.Headers["X-Frame-Options"] = "DENY"; // Prevent the page from being displayed in a frame, protecting against clickjacking attacks
            context.Response.Headers["X-XSS-Protection"] = "1; mode=block"; // Enable XSS protection in browsers
            context.Response.Headers["Referrer-Policy"] = "strict-origin-when-cross-origin"; // Control the information sent in the Referer header
            context.Response.Headers["Permissions-Policy"] = "camera=(), microphone=(), geolocation=()"; // Disable camera, microphone, and geolocation access by default
            context.Response.Headers["Cross-Origin-Resource-Policy"] = "same-origin"; // Restrict cross-origin resource sharing to same-origin only
            context.Response.Headers["Cross-Origin-Opener-Policy"] = "same-origin"; // Ensure that the resource can only be opened by same-origin documents, preventing cross-origin isolation issues
            context.Response.Headers["Cross-Origin-Embedder-Policy"] = "require-corp"; // Ensure that the resource can only be embedded by same-origin or cross-origin resources that explicitly allow it
            if (context.Request.IsHttps)
            {
                context.Response.Headers["Strict-Transport-Security"] = "max-age=31536000; includeSubDomains"; // Enforce HTTPS for one year
            }

            await _next(context);
        }
    }
}