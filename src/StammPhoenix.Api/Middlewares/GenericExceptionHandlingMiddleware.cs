using StammPhoenix.Api.Core;
using StammPhoenix.Domain.Exceptions;

namespace StammPhoenix.Api.Middlewares;

public sealed class GenericExceptionHandlingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            int statusCode = StatusCodes.Status500InternalServerError;

            if (e is StatusCodeException statusCodeException)
            {
                statusCode = statusCodeException.StatusCode;
            }

            var errorData = new ProblemData
            {
                Type = e.GetType().Name,
                Code = statusCode,
                Message = e.Message,
                Data = e.Data
            };

            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsJsonAsync(errorData);

            throw;
        }
    }
}
