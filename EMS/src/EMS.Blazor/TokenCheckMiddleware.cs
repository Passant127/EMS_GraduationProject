using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace EMS.Blazor
{
    public class TokenCheckMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenCheckMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Check if the Authorization header exists
            if (context.Request.Headers.TryGetValue("Authorization", out var authHeader))
            {
                context.Response.Cookies.Delete(".AspNetCore.Identity.Application");
            }



            await _next(context);
        }
    }
}
