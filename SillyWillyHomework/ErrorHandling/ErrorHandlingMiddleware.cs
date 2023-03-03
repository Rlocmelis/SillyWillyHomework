using FluentValidation;
using FluentValidation.Results;
using Newtonsoft.Json;
using SillyWillyHomework.Exceptions;
using SillyWillyHomework.Validation;
using System.Net;

namespace SillyWillyHomework.ErrorHandling
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException ex)
            {
                await HandleNotFoundExceptionAsync(context, ex);
            }
            catch (ValidationException ex)
            {
                await HandleValidationExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleGenericExceptionAsync(context, ex);
            }
        }

        private static async Task HandleNotFoundExceptionAsync(HttpContext context, NotFoundException exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.NotFound;
            var error = JsonConvert.SerializeObject(new { message = exception.Message });
            await response.WriteAsJsonAsync(error);
        }

        private static async Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.BadRequest;
            var errors = exception.Errors;

            context.Response.StatusCode = 400;
            var validationResponse = new ValidationErrorResponse(
                 errors.Select(error => new ValidationError
                 {
                     PropertyName = error.PropertyName,
                     ErrorMessage = error.ErrorMessage
                 })
             );

            await response.WriteAsJsonAsync(validationResponse);
        }

        private static async Task HandleGenericExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var error = JsonConvert.SerializeObject(new { message = exception.Message });
            await response.WriteAsJsonAsync(error);
        }
    }
}
