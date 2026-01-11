using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Maxiprod.UI.Filters
{
    /// <summary>
    /// Exception filter that handles domain-specific exceptions and returns appropriate HTTP responses.
    /// </summary>
    /// <remarks>
    /// Currently, it handles <see cref="ArgumentException"/> and returns a 400 Bad Request
    /// with the exception message as the response body.
    /// </remarks>
    public class DomainExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// Called when an exception is thrown during the execution of an action.
        /// </summary>
        /// <param name="context">The context in which the exception occurred.</param>
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ArgumentException)
            {
                context.Result = new BadRequestObjectResult(new
                {
                    error = context.Exception.Message
                });

                context.ExceptionHandled = true;
            }
        }
    }
}
