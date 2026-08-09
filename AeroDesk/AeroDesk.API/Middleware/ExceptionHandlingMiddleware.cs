using AeroDesk.Application.Common.Exceptions;
using System.Net;
using System.Text.Json;

namespace AeroDesk.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
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

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            object response;

            switch (exception)
            {
                case AeroDesk.Application.Common.Exceptions.ValidationException validationEx:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest; // 400
                    response = new
                    {
                        Success = false,
                        Message = "One or more validation errors occurred.",
                        Errors = validationEx.Errors
                    };
                    break;

                case UnauthorizedException:
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized; // 401
                    response = new
                    {
                        Success = false,
                        Message = exception.Message
                    };
                    break;

                case ForbiddenAccessException:
                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden; // 403
                    response = new
                    {
                        Success = false,
                        Message = exception.Message
                    };
                    break;

                case KeyNotFoundException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound; // 404
                    response = new
                    {
                        Success = false,
                        Message = exception.Message
                    };
                    break;

                default:
                    _logger.LogError(exception, "Unhandled exception occurred.");
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; // 500
                    response = new
                    {
                        Success = false,
                        Message = "An unexpected error occurred. Please try again later."
                    };
                    break;
            }

            var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(json);
        }
    }
}