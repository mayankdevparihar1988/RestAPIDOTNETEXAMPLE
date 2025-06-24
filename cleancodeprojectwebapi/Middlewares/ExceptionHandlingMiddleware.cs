using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace cleancodeprojectwebapi.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {   // Here we want control to be back to the next middleware in the pipeline
                // This is where the actual request processing happens
                // If an exception occurs, it will be caught by the catch block below
                // You can also add pre-processing logic here if needed
                await _next(httpContext);
            }
            catch (Exception)
            {
                // Log the exception (you can use a logging framework here)
                // For example: _logger.LogError(ex, "An unhandled exception occurred.");
                httpContext.Response.StatusCode = 500; // Internal Server Error
                httpContext.Response.ContentType = "application/json";
                var errorResponse = new { message = "An unexpected error occurred." };
                await httpContext.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
