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
        await SeedEntitiesAsync<Auction>(DummyData.Auctions);
        await SeedEntitiesAsync<Bid>(DummyData.Bids);
    }

    private async Task SeedEntitiesAsync<TEntity>(IEnumerable<TEntity> entities) 
        where TEntity : class
    {
        await _context.Set<TEntity>()
            .AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }
}
