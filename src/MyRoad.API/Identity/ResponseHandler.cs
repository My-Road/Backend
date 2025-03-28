using Microsoft.AspNetCore.Mvc;
using ErrorOr;
using MyRoad.API.Extensions;

namespace MyRoad.API.Identity
{
    public static class ResponseHandler
    {
        public static IActionResult HandleResult<T>(ErrorOr<T> response)
        {
            return response.IsError ? response.ToProblemDetails() : new OkObjectResult(response.Value);
        }
    }
}