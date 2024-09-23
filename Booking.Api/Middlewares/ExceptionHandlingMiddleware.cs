
using Booking.Domain.Enum;
using Booking.Domain.Result;
using System.Net;

namespace Booking.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next = null!;
        private readonly Serilog.ILogger _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, Serilog.ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExeptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExeptionAsync(HttpContext httpContext, Exception exception)
        {
            _logger.Error(exception, exception.Message);

            var responce = exception switch
            {
                ApplicationException _ => new BaseResult() { ErrorMessage = exception.Message, ErrorCode = (int)HttpStatusCode.Unauthorized },
                // _ => new BaseResult() { ErrorMessage = "Internal server error. Please retry later", ErrorCode = (int)HttpStatusCode.InternalServerError}
                _ => new BaseResult() { ErrorMessage = exception.Message + "/n" + exception, ErrorCode = (int)HttpStatusCode.InternalServerError}
            };
            //ApplicationException _ => new BaseResult() { ErrorMessage = exception.Message, ErrorCode = (int)HttpStatusCode.InternalServerError }
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)(responce.ErrorCode ?? 0);
            await httpContext.Response.WriteAsJsonAsync(responce);
        }

    }
}
