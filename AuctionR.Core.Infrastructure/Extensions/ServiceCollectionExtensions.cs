using AuctionR.Core.Domain.Interfaces;
using AuctionR.Core.Infrastructure.Persistance;
using AuctionR.Core.Infrastructure.Jobs;
using AuctionR.Core.Infrastructure.Seeders;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AuctionR.Core.Infrastructure.Jobs.Scheduling;
using AuctionR.Core.Infrastructure.Settings;

namespace AuctionR.Core.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<BidSettings>(configuration.GetSection("BidSettings"));
        services.Configure<BidSettings>(configuration.GetSection("CronSettings"));

        var defaultConnection = configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<AuctionRDbContext>(options =>
            options.UseSqlServer(defaultConnection)
        );

        services.AddHangfire(config =>
        {
            config.UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(defaultConnection);
        });

        services.AddHangfireServer();
        services.AddScoped<IAuctionStatusJob, AuctionStatusJob>();
        services.AddSingleton<IJobScheduler, HangfireJobScheduler>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ISeeder, Seeder>();
    }
}