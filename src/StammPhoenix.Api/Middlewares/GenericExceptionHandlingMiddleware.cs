using StammPhoenix.Api.Core;

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
            var errorData = new ProblemData
            {
                Type = e.GetType().Name,
                Code = StatusCodes.Status500InternalServerError,
                Message = e.Message,
                Data = e.Data
            };

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await context.Response.WriteAsJsonAsync(errorData);

            throw;
        }
    }
}
