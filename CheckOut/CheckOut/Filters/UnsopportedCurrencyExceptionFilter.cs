using Checkout.Model.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CheckOut.Filters
{
    public class UnsopportedCurrencyExceptionFilter : ExceptionFilterAttribute
    {
        public ILogger<UnsopportedCurrencyExceptionFilter> Logger { get; }
        public UnsopportedCurrencyExceptionFilter(ILogger<UnsopportedCurrencyExceptionFilter> logger)
        {
            Logger = logger;
        }

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            if (context != null && !context.ExceptionHandled && context.Exception is UnsopportedCurrencyException)
            {
                Logger.LogError(context.Exception as UnsopportedCurrencyException, "Unsupported Currency Type!", context.Exception.Message);
                context.Result = new ContentResult
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Content = "Unsupported Currency Type",
                    ContentType = "text/plain",
                };
                context.ExceptionHandled = true;
            }
            return base.OnExceptionAsync(context);
        }
    }
}
