using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NotificationSchedule.Contracts.Models.Exceptions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace NotificationScheduleSystem.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string returnMessage;
            if (exception.GetType() == typeof(ServiceUnavailableException))
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                returnMessage = "Service Unavailable!";
            }

            else if (exception.GetType() == typeof(BadRequestException))
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                returnMessage = exception.Message;
            }

            else
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                returnMessage = "An error occured while processing the request!";
            }
            returnMessage = JsonConvert.SerializeObject(returnMessage);
            return context.Response.WriteAsync(returnMessage);
        }
    }
}
