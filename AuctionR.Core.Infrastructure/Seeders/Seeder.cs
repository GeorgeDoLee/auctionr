using AuctionR.Core.Domain.Entities;
using AuctionR.Core.Infrastructure.Persistance;

namespace AuctionR.Core.Infrastructure.Seeders;

internal class Seeder : ISeeder
{
    private readonly AuctionRDbContext _context;

    public Seeder(AuctionRDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        var auctions = DummyData.GetAuctions();
        await SeedEntitiesAsync<Auction>(auctions);
        await SeedEntitiesAsync<Bid>(DummyData.GetBids(auctions));
    }

    private async Task SeedEntitiesAsync<TEntity>(IEnumerable<TEntity> entities) 
        where TEntity : class
    {
        if (!_context.Set<TEntity>().Any())
        {
            await _context.Set<TEntity>()
                .AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }
    }
}
