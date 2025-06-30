using MyRoad.Domain.Identity.Interfaces;
using MyRoad.Domain.Users;

namespace MyRoad.API.Middlewares;

public class TokenVersionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, IUserService userService, IUserContext userContext)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            try
            {
                var userId = userContext.Id;
                var tokenVersionClaim = userContext.TokenVersion;

                if (userId == 0 || tokenVersionClaim == null)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Invalid token claims.");
                    return;
                }

                var userResult = await userService.GetByIdAsync(userId);

                if (userResult.IsError)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("error occurred.");
                    return;
                }

                var user = userResult.Value;
                if (user.TokenVersion != tokenVersionClaim)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Token is no longer valid login again.");
                    return;
                }
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Authentication error: " + ex.Message);
                return;
            }
        }

        await next(context);
    }
}