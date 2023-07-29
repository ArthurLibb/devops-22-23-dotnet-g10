using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System;
using System.Threading.Tasks;
using Shared.Infrastructrure;
using Domain.Exceptions;

namespace Server.Middleware;

public class ExceptionMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly RequestDelegate next;

    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, RequestDelegate next)
    {
        _logger = logger;
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something went wrong");
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        ErrorDetails error = exception switch
        {
            EntityNotFoundException ex => new ErrorDetails(ex.Message, HttpStatusCode.NotFound),
            EntityAlreadyExistsException ex => new ErrorDetails(ex.Message, HttpStatusCode.Conflict),
            // Add more custom exceptions here...
            ApplicationException ex => new ErrorDetails(ex.Message),
            _ => new ErrorDetails(exception.Message)
        };
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)error.StatusCode;
        await context.Response.WriteAsync(error.ToString());
    }
}
