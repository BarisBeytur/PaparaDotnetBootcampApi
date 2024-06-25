using Newtonsoft.Json;
using System.Net;

namespace PaparaDotnetBootcampApi.Middlewares
{

    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;


        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            if (exception is ArgumentException) code = HttpStatusCode.BadRequest;
            else if (exception is KeyNotFoundException) code = HttpStatusCode.NotFound;

            var result = JsonConvert.SerializeObject(new { status = (int)code, message = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }

    }
}
