using System.Collections.Concurrent;
using Microsoft.AspNetCore.Mvc;

namespace MyRoad.API.Middlewares;

public class RateLimitingMiddleware(RequestDelegate next)
{
    private static readonly ConcurrentDictionary<string, ClientRequestInfo> Clients = new();
    private const int MaxRequestsPerMinute = 10;
    private static readonly TimeSpan TimeWindow = TimeSpan.FromMinutes(1);

    public async Task InvokeAsync(HttpContext context)
    {
        var ip = context.Connection.RemoteIpAddress?.ToString();
        var path = context.Request.Path.ToString();

        if (string.IsNullOrWhiteSpace(ip))
        {
            await next(context);
            return;
        }

        var key = $"{ip}:{path}";
        var now = DateTime.UtcNow;

        var client = Clients.GetOrAdd(key, _ => new ClientRequestInfo(now));

        lock (client)
        {
            if (now - client.FirstRequestTime < TimeWindow)
            {
                client.RequestCount++;

                if (client.RequestCount > MaxRequestsPerMinute)
                {
                    var problemDetails = new ProblemDetails
                    {
                        Status = StatusCodes.Status429TooManyRequests,
                        Title = "Too Many Requests",
                        Detail = "Request limit exceeded for this endpoint.",
                        Type = "https://httpstatuses.com/429"
                    };

                    context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    context.Response.Headers.RetryAfter =
                        (TimeWindow - (now - client.FirstRequestTime)).TotalSeconds.ToString("F0");

                    context.Response.WriteAsJsonAsync(problemDetails);
                    return;
                }
            }
            else
            {
                client.FirstRequestTime = now;
                client.RequestCount = 1;
            }
        }

        await next(context);
    }

    private class ClientRequestInfo(DateTime now)
    {
        public DateTime FirstRequestTime { get; set; } = now;
        public int RequestCount { get; set; } = 1;
    }
}