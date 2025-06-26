using System.Net;
using System.Text.Json;
using ApplicationBusinessLayer.Exceptions;
using Azure;

namespace FrameworkDriversAPI.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
                await _next(httpContext);
        }
        catch (ValidationException e)
        {
            await HandleExceptionAsync(httpContext, e);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, ValidationException exception)
    {
        var response = context.Response;
            response.ContentType = "application/json";
        var statusCode = HttpStatusCode.InternalServerError;

        var result = JsonSerializer.Serialize(new
        {
            error=exception.Message,
            detail=exception.InnerException?.Message,
        });
        
        response.StatusCode = (int)statusCode;
        await response.WriteAsync(result);
    }
}