using ProductsOrders.Domain.Errors;
using ProductsOrders.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace ProductsOrders.Api.Middlewares;

public class GlobalErrorHandlerMiddleware(RequestDelegate next, IHostEnvironment env)
{
    private readonly RequestDelegate _next = next;
    private readonly IHostEnvironment _env = env;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = exception switch
        {
            NotFoundException => (int) HttpStatusCode.NotFound,
            InvalidCredentialsException => (int)HttpStatusCode.BadRequest,
            ExternalProviderException => (int)HttpStatusCode.BadGateway,
            ProviderNotSupported => (int)HttpStatusCode.BadRequest,
            _ => (int) HttpStatusCode.InternalServerError,
        };
        
        var response = new ErrorResponse { 
            Code = statusCode, 
            Message = exception.Message,
            StackTrace = _env.IsDevelopment() ? exception.StackTrace : ""
        };

        var payload = JsonSerializer.Serialize(response);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        return context.Response.WriteAsync(payload);
    }
}
