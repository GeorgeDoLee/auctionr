using AuctionR.Core.Domain.Entities;
using AuctionR.Shared.Interfaces;

namespace AuctionR.Core.Domain.Interfaces;

public interface IBidRepository : IRepository<Bid>
{
    Task<IEnumerable<Bid>> GetByAuctionIdAsync(
        int auctionId, int pageNumber, int pageSize, 
        CancellationToken ct = default);
}
