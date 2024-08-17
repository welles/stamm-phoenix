using FastEndpoints;
using StammPhoenix.Domain.Exceptions;

namespace StammPhoenix.Api.Middlewares;

public class DomainExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public DomainExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DomainException e)
        {
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Detail = e.Message
            };

            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}
