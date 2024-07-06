using PaparaDotnetBootcampApi.Business.Services.Abstract;

namespace PaparaDotnetBootcampApi.Extensions.Middlewares
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            var username = context.Request.Headers["Username"].FirstOrDefault();
            var password = context.Request.Headers["Password"].FirstOrDefault();

            if (username != null && password != null)
            {
                var user = userService.Authenticate(username, password);
                if (user != null)
                {
                    context.Items["User"] = user;
                }
            }

            await _next(context);
        }
    }
}
