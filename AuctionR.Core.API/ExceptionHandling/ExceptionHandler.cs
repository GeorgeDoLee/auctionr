using AuctionR.Core.API.Constants;
using AuctionR.Core.Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace AuctionR.Core.API.ExceptionHandling;

public static class ExceptionHandler
{
    public static async Task HandleValidationExceptionAsync(HttpContext context, ValidationException ex)
    {
        var validationErrors = ex.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.ErrorMessage).ToArray()
            );

        var problemDetails = new ValidationProblemDetails(validationErrors)
        {
            Type = ProblemTypeUrls.BadRequest,
            Title = "One or more validation errors occurred.",
            Status = StatusCodes.Status400BadRequest,
            Detail = "See the errors property for details.",
            Instance = context.Request.Path
        };

        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        context.Response.ContentType = "application/problem+json";
        await context.Response.WriteAsJsonAsync(problemDetails);
    }

    public static async Task HandleNotFoundExceptionAsync(HttpContext context, NotFoundException ex)
    {
        var problemDetails = new ProblemDetails
        {
            Type = ProblemTypeUrls.NotFound,
            Title = "Resource not found.",
            Status = StatusCodes.Status404NotFound,
            Detail = ex.Message,
            Instance = context.Request.Path
        };

        context.Response.StatusCode = StatusCodes.Status404NotFound;
        context.Response.ContentType = "application/problem+json";
        await context.Response.WriteAsJsonAsync(problemDetails);
    }

    public static async Task HandleInvalidOperationExceptionAsync(HttpContext context, InvalidOperationException ex)
    {
        var problemDetails = new ProblemDetails
        {
            Type = ProblemTypeUrls.BadRequest,
            Title = "Invalid operation.",
            Status = StatusCodes.Status400BadRequest,
            Detail = ex.Message,
            Instance = context.Request.Path
        };

        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        context.Response.ContentType = "application/problem+json";
        await context.Response.WriteAsJsonAsync(problemDetails);
    }

    public static async Task HandleUnauthorizedExceptionAsync(HttpContext context)
    {
        var problem = new ProblemDetails
        {
            Type = ProblemTypeUrls.Unauthorized,
            Title = "Unauthorized",
            Status = StatusCodes.Status401Unauthorized,
            Detail = "Authentication is required or the provided token is invalid.",
            Instance = context.Request.Path
        };

        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        context.Response.ContentType = "application/problem+json";
        await context.Response.WriteAsJsonAsync(problem);
    }

    public static async Task HandleForbiddenExceptionAsync(HttpContext context, ForbiddenException ex)
    {
        var problem = new ProblemDetails
        {
            Type = ProblemTypeUrls.Forbidden,
            Title = "Forbidden",
            Status = StatusCodes.Status403Forbidden,
            Detail = ex.Message,
            Instance = context.Request.Path
        };

        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        context.Response.ContentType = "application/problem+json";
        await context.Response.WriteAsJsonAsync(problem);
    }

    public static async Task HandleRateLimitExceptionAsync(HttpContext context)
    {
        var problem = new ProblemDetails
        {
            Type = ProblemTypeUrls.TooManyRequests,
            Title = "Too Many Requests",
            Status = StatusCodes.Status429TooManyRequests,
            Detail = "You have exceeded the allowed number of requests. Please try again later.",
            Instance = context.Request.Path
        };

        context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
        context.Response.ContentType = "application/problem+json";

        await context.Response.WriteAsJsonAsync(problem);
    }

    public static async Task HandleArgumentExceptionAsync(HttpContext context, ArgumentException ex)
    {
        var problemDetails = new ProblemDetails
        {
            Type = ProblemTypeUrls.BadRequest,
            Title = "Invalid argument.",
            Status = StatusCodes.Status400BadRequest,
            Detail = ex.Message,
            Instance = context.Request.Path
        };

        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        context.Response.ContentType = "application/problem+json";
        await context.Response.WriteAsJsonAsync(problemDetails);
    }


    public static async Task HandleUnexpectedExceptionAsync(HttpContext context)
    {
        var problemDetails = new ProblemDetails
        {
            Type = ProblemTypeUrls.InternalServerError,
            Title = "An unexpected error occurred.",
            Status = StatusCodes.Status500InternalServerError,
            Detail = "Please try again later or contact support.",
            Instance = context.Request.Path
        };

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/problem+json";
        await context.Response.WriteAsJsonAsync(problemDetails);
    }
}
