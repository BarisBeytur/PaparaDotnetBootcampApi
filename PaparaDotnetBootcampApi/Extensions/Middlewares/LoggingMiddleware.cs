namespace PaparaDotnetBootcampApi.Extensions.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            // Request bilgilerini loglama
            _logger.LogInformation($"Request: {context.Request.Method} {context.Request.Path}");

            // Response bilgilerini loglama
            Stream originalBody = context.Response.Body;

            try
            {
                using (var responseBody = new MemoryStream())
                {
                    context.Response.Body = responseBody;

                    await _next(context);

                    // Response içeriğini okuma ve loglama
                    responseBody.Seek(0, SeekOrigin.Begin);
                    string responseBodyContent = await new StreamReader(responseBody).ReadToEndAsync();
                    _logger.LogInformation($"Response: {context.Response.StatusCode}\n{responseBodyContent}");

                    responseBody.Seek(0, SeekOrigin.Begin);
                    await responseBody.CopyToAsync(originalBody);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {ex.Message}");
                throw;
            }
            finally
            {
                context.Response.Body = originalBody;
            }
        }
    }
}
