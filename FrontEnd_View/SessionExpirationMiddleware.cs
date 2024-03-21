using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FrontEnd_View
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class SessionExpirationMiddleware
    {
        private readonly RequestDelegate _next;

        public SessionExpirationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Session.GetString("staffName") != null)
            {
                context.Response.Redirect("/Shared/Error"); // Redirect to the error page
                return;
            }

            await _next(context);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class SessionExpirationMiddlewareExtensions
    {
        public static IApplicationBuilder UseSessionExpirationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SessionExpirationMiddleware>();
        }
    }
}
