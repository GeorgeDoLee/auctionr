using AuctionR.Core.Domain.Entities;
using AuctionR.Shared.Interfaces;

namespace AuctionR.Core.Domain.Interfaces;

public interface IAuctionRepository : IRepository<Auction>
{
    Task<Auction?> GetWithBidsAsync(int id, CancellationToken ct);
}
