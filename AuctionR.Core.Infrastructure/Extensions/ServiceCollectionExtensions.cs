using AuctionR.Core.Domain.Interfaces;
using AuctionR.Core.Infrastructure.Persistance;
using AuctionR.Core.Infrastructure.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuctionR.Core.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AuctionRDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
        );

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ISeeder, Seeder>();
    }
}