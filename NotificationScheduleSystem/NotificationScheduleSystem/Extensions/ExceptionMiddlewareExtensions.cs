using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using NotificationScheduleSystem.API.Middleware;

namespace NotificationScheduleSystem.API.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger logger)
        {
            app.UseMiddleware<ExceptionMiddleware>(logger);
        }
    }
}
