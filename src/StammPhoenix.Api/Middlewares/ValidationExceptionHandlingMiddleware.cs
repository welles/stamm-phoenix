using FluentValidation;
using StammPhoenix.Api.Core;

namespace StammPhoenix.Api.Middlewares;

public sealed class ValidationExceptionHandlingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException exception)
        {
            var validationErrors = exception.Errors.ToDictionary(
                x => x.PropertyName,
                x => x.ErrorMessage
            );

            var errorData = new ProblemData
            {
                Type = nameof(ValidationException),
                Code = StatusCodes.Status400BadRequest,
                Message = "One or more validation errors has occurred",
                Data = validationErrors,
            };

            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            await context.Response.WriteAsJsonAsync(errorData);

            throw;
        }
    }
}
