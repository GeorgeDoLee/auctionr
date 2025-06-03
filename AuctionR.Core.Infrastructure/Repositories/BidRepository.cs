using AuctionR.Core.Domain.Entities;
using AuctionR.Core.Domain.Interfaces;
using AuctionR.Core.Infrastructure.Persistance;
using AuctionR.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AuctionR.Core.Infrastructure.Repositories;

internal class BidRepository : Repository<AuctionRDbContext, Bid>, IBidRepository
{
    public BidRepository(AuctionRDbContext context) 
        : base(context)
    {
    }

    public async Task<IEnumerable<Bid>> GetByAuctionIdAsync(
        int auctionId, int pageNumber, int pageSize, CancellationToken ct = default)
    {
        return await _context.Bids
            .Where(b => b.AuctionId == auctionId)
            .OrderByDescending(b => b.Timestamp)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);
    }
}
