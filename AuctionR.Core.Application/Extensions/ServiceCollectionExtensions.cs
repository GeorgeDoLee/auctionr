using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
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

        services.AddValidatorsFromAssembly(appAssambly)
            .AddFluentValidationAutoValidation();

        TypeAdapterConfig.GlobalSettings
            .Scan(typeof(ServiceCollectionExtensions).Assembly);

        return services;
    }
}