
using Serilog;

namespace API.Project.Middlewares
{
    public class AnotherMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Log.Information("Before Another Middleware");
            await next(context);
            Log.Information("After Another Middleware");
        }
    }
}
