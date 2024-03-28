using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace WebhookSample.API.Filters
{
    public class GlobalExceptionFilters : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (!context.ExceptionHandled)
            {
                var exception = context.Exception;
                int statusCode;
                object message;
                switch (true)
                {
                    case bool _ when exception is UnauthorizedAccessException:
                        statusCode = (int)HttpStatusCode.Unauthorized;
                        message = exception.Message;
                        break;

                    case bool _ when exception is InvalidOperationException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        message = exception.Message;
                        break;

                    case bool _ when exception is ValidationException:
                        var validationException = exception as ValidationException;
                        statusCode = (int)HttpStatusCode.UnprocessableEntity;
                        message = validationException.Errors.Select(x => x.ErrorMessage);
                        break;

                    case bool _ when exception is KeyNotFoundException:
                        statusCode = (int)HttpStatusCode.NotFound;
                        message = exception.Message;
                        break;

                    default:
                        statusCode = (int)HttpStatusCode.InternalServerError;
                        message = "An unexpected error happened";
                        break;
                }
                // _logger.LogError($"GlobalExceptionFilter: Error in {context.ActionDescriptor.DisplayName}. {exception.Message}. Stack Trace: {exception.StackTrace}");
                // Custom Exception message to be returned to the UI
                context.Result = new ObjectResult(message) { StatusCode = statusCode };
            }
        }
    }
}
