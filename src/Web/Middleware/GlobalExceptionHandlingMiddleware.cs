using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using SpendWise.Core.Exceptions;

namespace SpendWise.Web.Middleware
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (AppValidationException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest,
                    "https://spendwiseapi/errors/validation", "Validation Error");
            }
            catch (NotFoundException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.NotFound,
                    "https://spendwiseapi/errors/notfound", "Not Found");
            }
            catch (UnauthorizedAccessException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.Unauthorized,
                    "https://spendwiseapi/errors/unauthorized", "Unauthorized");
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError,
                    "https://spendwiseapi/errors/server", "Internal Server Error", 
                    "An unexpected error occurred.");
            }
        }

        private async Task HandleExceptionAsync(
            HttpContext context,
            Exception ex,
            HttpStatusCode statusCode,
            string type,
            string title,
            string? detail = null)
        {
            _logger.LogError(ex, ex.Message);

            var problem = new ProblemDetails
            {
                Status = (int)statusCode,
                Type = type,
                Title = title,
                Detail = detail ?? ex.Message
            };

            string json = JsonSerializer.Serialize(problem);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            await context.Response.WriteAsync(json);
        }
    }
}
