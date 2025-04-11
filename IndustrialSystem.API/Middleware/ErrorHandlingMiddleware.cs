using System.Net;
using System.Text.Json;
using IndustrialSystem.Services.Exceptions;
using Microsoft.AspNetCore.Http;

namespace IndustrialSystem.API.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;
    private readonly IWebHostEnvironment _env;

    public ErrorHandlingMiddleware(
        RequestDelegate next,
        ILogger<ErrorHandlingMiddleware> logger,
        IWebHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

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

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;
        response.ContentType = "application/json";
        var result = new ErrorResponse { Success = false };

        switch (exception)
        {
            case UnauthorizedException:
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
                result.Message = exception.Message;
                break;

            case NotFoundException:
                response.StatusCode = (int)HttpStatusCode.NotFound;
                result.Message = exception.Message;
                break;

            case ValidationException validationEx:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                result.Message = "Erreurs de validation";
                result.Errors = validationEx.Errors;
                break;

            case BadRequestException:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                result.Message = exception.Message;
                break;

            case ConflictException:
                response.StatusCode = (int)HttpStatusCode.Conflict;
                result.Message = exception.Message;
                break;

            case BusinessRuleException:
                response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                result.Message = exception.Message;
                break;

            case ForeignKeyViolationException:
                response.StatusCode = (int)HttpStatusCode.Conflict;
                result.Message = exception.Message;
                break;

            case DuplicateEntryException:
                response.StatusCode = (int)HttpStatusCode.Conflict;
                result.Message = exception.Message;
                break;

            case SecurityException:
                response.StatusCode = (int)HttpStatusCode.Forbidden;
                result.Message = exception.Message;
                break;

            case ConcurrencyException:
                response.StatusCode = (int)HttpStatusCode.Conflict;
                result.Message = exception.Message;
                break;

            default:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                result.Message = _env.IsDevelopment() 
                    ? exception.Message 
                    : "Une erreur interne s'est produite.";
                _logger.LogError(exception, "Une erreur non gérée s'est produite");
                break;
        }

        if (_env.IsDevelopment())
        {
            result.DeveloperMessage = new DeveloperMessage
            {
                Exception = exception.GetType().Name,
                StackTrace = exception.StackTrace,
                InnerException = exception.InnerException?.Message
            };
        }

        var jsonResult = JsonSerializer.Serialize(result, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await response.WriteAsync(jsonResult);
    }
}

public class ErrorResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public IDictionary<string, string[]>? Errors { get; set; }
    public DeveloperMessage? DeveloperMessage { get; set; }
}

public class DeveloperMessage
{
    public string? Exception { get; set; }
    public string? StackTrace { get; set; }
    public string? InnerException { get; set; }
}

// Extension method pour faciliter l'ajout du middleware
public static class ErrorHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseErrorHandling(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandlingMiddleware>();
    }
}