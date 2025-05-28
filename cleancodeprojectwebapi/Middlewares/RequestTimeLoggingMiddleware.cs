using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace cleancodeprojectwebapi.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class RequestTimeLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestTimeLoggingMiddleware> _logger;

        public RequestTimeLoggingMiddleware(RequestDelegate next, ILogger<RequestTimeLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var stopWatch = Stopwatch.StartNew();
            await _next(httpContext);

            stopWatch.Stop();
            if (stopWatch.ElapsedMilliseconds / 1000 > 4)
            {
                _logger.LogInformation("Request [{Verb}] at {Path} took {Time} ms",
                    httpContext.Request.Method,
                    httpContext.Request.Path,
                    stopWatch.ElapsedMilliseconds);
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class RequestTimeLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestTimeLoggingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestTimeLoggingMiddleware>();
        }
    }
}
