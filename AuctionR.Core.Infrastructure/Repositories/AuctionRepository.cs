using AuctionR.Core.Domain.Entities;
using AuctionR.Core.Domain.Interfaces;
using AuctionR.Core.Infrastructure.Persistance;
using AuctionR.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AuctionR.Core.Infrastructure.Repositories;

internal class AuctionRepository : Repository<AuctionRDbContext, Auction>, IAuctionRepository
{

    public AuctionRepository(AuctionRDbContext context) 
        : base(context)
    {
    }

    public override async Task<Auction?> GetAsync(int id, CancellationToken ct = default)
    {
        return await _context.Auctions
            .Include(a => a.Bids)
            .FirstOrDefaultAsync(a => a.Id == id, ct);
    }
}
