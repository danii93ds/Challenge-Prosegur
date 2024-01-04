using System.Net;
using System.Text.Json;

namespace Challenge_Prosegur.Exceptions
{
    public class ErrorHandler
    {
        private readonly RequestDelegate Next;
        private readonly ILogger Logger;

        public ErrorHandler(RequestDelegate next, ILogger<ErrorHandler> logger)
        {
            Next = next;
            Logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await Next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case AppException e:
                        response.StatusCode = (int)HttpStatusCode.Conflict; break;
                    case KeyNotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound; break;
                    default:
                        Logger.LogError(error, error.Message);
                        response.StatusCode = (int)HttpStatusCode.InternalServerError; break;
                }

                var resultado = JsonSerializer.Serialize(new { message = error?.Message });
                await response.WriteAsync(resultado);
            }
        }
    }
}
