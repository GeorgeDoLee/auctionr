using AuctionR.Core.Application.Common.Behavior;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AuctionR.Core.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var appAssambly = typeof(ServiceCollectionExtensions).Assembly;

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(appAssambly);
        });

        services.AddValidatorsFromAssembly(appAssambly);

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        TypeAdapterConfig.GlobalSettings
            .Scan(typeof(ServiceCollectionExtensions).Assembly);

        return services;
    }
}