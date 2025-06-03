using Serilog;
using System.Threading.RateLimiting;

namespace AuctionR.Core.API.Extensions;

public static class WebAppliactionBuilderExtensions
{
    public static WebApplicationBuilder AddPresentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddSignalR();

        builder.Host.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration)
        );

        builder.Services.AddRateLimiter(options =>
        {
            var rateLimitConfig = builder.Configuration.GetSection("RateLimiting");
            int permitLimit = rateLimitConfig.GetValue<int>("PermitLimit");
            int windowSeconds = rateLimitConfig.GetValue<int>("WindowSeconds");

            options.AddPolicy("Fixed", httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = permitLimit,
                        Window = TimeSpan.FromSeconds(windowSeconds),
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        QueueLimit = 0
                    }));

            options.OnRejected = async (context, _) =>
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                context.HttpContext.Response.ContentType = "text/plain";
                await context.HttpContext.Response.WriteAsync("Too many requests. try again later.");
            };
        });

        return builder;
    }
}
