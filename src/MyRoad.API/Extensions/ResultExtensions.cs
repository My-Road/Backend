using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace MyRoad.API.Extensions;

public static class ResultExtensions
{
    public static IActionResult ToProblemDetails<T>(this ErrorOr<T> result)
    {
        if (!result.IsError)
            throw new InvalidOperationException("Result is success");

        var statusCode = GetStatusCode(result.Errors);

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = GetTitle(statusCode),
            Detail = string.Join("; ", result.Errors.Select(e => $"{e.Code}: {e.Description}")),
            Extensions = { { "errorCodes", result.Errors.Select(e => e.Code) } }
        };

        return new ObjectResult(problemDetails) { StatusCode = statusCode };
    }

    private static int GetStatusCode(List<Error> errors)
    {
        var errorToConsider = errors.First();

        var statusCode = errorToConsider.Type switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };
        return statusCode;
    }

    private static string GetTitle(int statusCode) =>
        statusCode switch
        {
            StatusCodes.Status400BadRequest => "Invalid request",
            StatusCodes.Status401Unauthorized => "Unauthorized",
            StatusCodes.Status404NotFound => "Resource not found",
            StatusCodes.Status409Conflict => "Conflict occurred",
            _ => "An unexpected error occurred"
        };
}