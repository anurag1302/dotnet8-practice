using Serilog;

namespace API.Project.Middlewares
{
    public class ConventionMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            Log.Information("Before Middleware");
            await next(context);
            Log.Information("After Middleware");
        }
    }
}
