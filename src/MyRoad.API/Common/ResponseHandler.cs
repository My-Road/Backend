using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using MyRoad.API.Extensions;

namespace MyRoad.API.Common
{
    public static class ResponseHandler
    {
        public static IActionResult HandleResult<T>(ErrorOr<T> response)
        {
            return response.IsError ? response.ToProblemDetails() : new OkObjectResult(response.Value);
        }
    }
}