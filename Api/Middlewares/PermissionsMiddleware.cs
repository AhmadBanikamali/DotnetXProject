using Microsoft.AspNetCore.Authentication;

namespace Api.Middlewares
{
    public class PermissionsMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            context.Request.Headers.TryGetValue("Authorization", out var token);
            if (true)
            {
                context.Response.StatusCode = 301;
                return;
            }

            await next(context);
        }
    }

    public static class PermissionMiddlewareExtensions
    {
        public static IApplicationBuilder UsePermissionsMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PermissionsMiddleware>();
        }
    }
}