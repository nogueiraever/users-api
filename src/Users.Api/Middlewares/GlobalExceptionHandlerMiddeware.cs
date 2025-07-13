using System.Net;
using System.Text.Json;
using Users.Application.Common;

namespace Users.Api.Middlewares
{
    public class GlobalExceptionHandlerMiddeware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<GlobalExceptionHandlerMiddeware> logger;

        public GlobalExceptionHandlerMiddeware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddeware> logger)
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                await HandleResponse(context);
            }
        }

        private async Task HandleResponse(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = Result.Failure("An internal server Error has occurred");


            var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
