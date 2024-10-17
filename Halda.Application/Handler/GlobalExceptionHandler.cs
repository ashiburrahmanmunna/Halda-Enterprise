using Halda.Core.Enums;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Halda.Application.Handler
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

            var statusCode = StatusCodes.Status500InternalServerError;

            if (exception is CustomException customException)
            {
                statusCode = customException.StatusCode;
            }

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Detail = exception.InnerException?.Message ?? "No additional details are available.",
                Title = exception.Message ?? "An error occurred.",
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1"
            };

            httpContext.Response.StatusCode = statusCode;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }



    }
}
