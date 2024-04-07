using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Net;
using WebhookSample.Domain.Responses;

namespace WebhookSample.API.Filters
{
    public class GlobalExceptionFilters : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilters> _logger;

        public GlobalExceptionFilters(ILogger<GlobalExceptionFilters> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (!context.ExceptionHandled)
            {
                var response = new ErrorResponse();
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
                response.AddErrorResponse(statusCode, new List<object> { message });
                _logger.LogError(exception, JsonConvert.SerializeObject(response));

                context.Result = new ObjectResult(response) { StatusCode = response.StatusCode };
            }
        }
    }
}
