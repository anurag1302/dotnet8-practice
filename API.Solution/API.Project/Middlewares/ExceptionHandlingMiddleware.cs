using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Text.Json;

namespace API.Project.Middlewares
{
    public class ExceptionHandlingMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);

            }
            catch (Exception ex)
            {
                Log.Error("Error Encountered.");
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = 500;

                var problemDetails = new ProblemDetails
                {
                    Title = ex.Message,
                    Status = 500
                };

                var result = JsonSerializer.Serialize(problemDetails);

                await response.WriteAsync(result);
            }
        }
    }
}
