using Checkout.Model.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CheckOut.Filters
{
    public sealed class InvalidCurrencyKeyExceptionFilter : ExceptionFilterAttribute
    {
        public ILogger<InvalidCurrencyKeyExceptionFilter> Logger { get; }

        public InvalidCurrencyKeyExceptionFilter(ILogger<InvalidCurrencyKeyExceptionFilter> logger)
        {
            Logger = logger;
        }

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            if (context != null && !context.ExceptionHandled && context.Exception is InvalidCurrencyKeyException)
            {
                Logger.LogError(context.Exception as InvalidCurrencyKeyException, "Failed to parse currency because of invalid currency value", context.Exception.Message);
                context.Result = new ContentResult
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Content = "Invalid currency values given!",
                    ContentType = "text/plain",
                };
                context.ExceptionHandled = true;
            }
            return base.OnExceptionAsync(context);
        }
    }
}
