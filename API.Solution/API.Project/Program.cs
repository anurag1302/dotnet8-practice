
using API.Project.Middlewares;
using API.Project.Stuffs;
using Serilog;

namespace API.Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddKeyedSingleton<IMessage, SingletonMessage>("singleton");
            builder.Services.AddKeyedSingleton<IMessage, ScopedMessage>("scoped");
            builder.Services.AddKeyedSingleton<IMessage, TransientMessage>("transient");

            builder.Services.AddTransient<AnotherMiddleware>();

            Log.Logger = new LoggerConfiguration()
                //.WriteTo.Console()
                //.WriteTo.File("log.txt", rollingInterval:RollingInterval.Day)
                .MinimumLevel.Information()
                .WriteTo.Seq("http://localhost:5341/")
                .CreateLogger();

            builder.Services.AddOptions<CountryData>().BindConfiguration("CountryData");

            builder.Services.AddCors(c =>
            {
                c.AddPolicy("CorsPolicy", opt =>
                {
                    opt.AllowAnyMethod().
                    AllowAnyOrigin().
                    AllowAnyHeader();
                });
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");

            app.UseAuthorization();
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseMiddleware<ConventionMiddleware>();
            app.UseMiddleware<AnotherMiddleware>();


            app.MapControllers();

            app.Run();
        }
    }
}
