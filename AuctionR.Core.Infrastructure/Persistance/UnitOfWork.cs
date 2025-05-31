using AuctionR.Core.Domain.Interfaces;
using AuctionR.Core.Infrastructure.Repositories;

namespace AuctionR.Core.Infrastructure.Persistance;

internal class UnitOfWork : IUnitOfWork
{
    private readonly AuctionRDbContext _context;

    public UnitOfWork(AuctionRDbContext context)
    {
        _context = context;
        Bids = new BidRepository(_context);
        Auctions = new AuctionRepository(_context);
    }

    public IBidRepository Bids { get; }

    public IAuctionRepository Auctions { get; }

    public async Task Complete(CancellationToken ct = default)
    {
        _ = await _context.SaveChangesAsync(ct);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
