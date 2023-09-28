using System.Net;
using System.Text.Json;
using TestTask.Exceptions;

namespace TestTask.Middleware
{
    public class CustomExceptionWare
    {
        private readonly RequestDelegate _next;

        public CustomExceptionWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case NotFoundException:
                        httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new { ex.Message }));
            }

        }
    }

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionWare(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionWare>();
        }
    }
}
