using DaySchedulerApp.Application.Exceptions;

namespace DaySchedulerApp.Api.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (NotFoundException notFoundException)
            {
                _logger.LogError(notFoundException.Message);

                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync(notFoundException.Message);
            }
            catch (BadRequestException badRequestException)
            {
                _logger.LogError(badRequestException.Message);

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync(badRequestException.Message);
            }
            catch (ValidationException validationException)
            {
                _logger.LogError(validationException.Message);

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync(validationException.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);

                context.Response.StatusCode = 500;

                await context.Response.WriteAsync("Something went wrong!");
            }
        }
    }
}