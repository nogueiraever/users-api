using Users.Infrastructure;
using Users.Application;
using Users.Api.Middlewares;

namespace Users.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();
            
            builder.Services
                .AddInfrastructure(builder.Configuration)
                .AddApplication();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseMiddleware<GlobalExceptionHandlerMiddeware>();

            app.MapControllers();

            app.Run();
        }
    }
}
