using System.Text.Json;
using Smartspend.Api.Exceptions;

namespace Smartspend.Api.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ConflictException ex)
        {
            await WriteResponse(context, StatusCodes.Status409Conflict, ex.Message);
        }
        catch (UnauthorizedException ex)
        {
        await WriteResponse(context, StatusCodes.Status401Unauthorized, ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            await WriteResponse(context, StatusCodes.Status400BadRequest, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");
            await WriteResponse(context, StatusCodes.Status500InternalServerError,
                "Something went wrong. Please try again later.");
        }
    }

    private static async Task WriteResponse(HttpContext context, int statusCode, string message)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        var body = JsonSerializer.Serialize(new { error = message });
        await context.Response.WriteAsync(body);
    }

}