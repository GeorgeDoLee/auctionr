using AuctionR.Core.Domain.Entities;
using AuctionR.Core.Domain.Interfaces;
using AuctionR.Core.Infrastructure.Persistance;
using AuctionR.Shared.Repositories;

namespace AuctionR.Core.Infrastructure.Repositories;

internal class BidRepository : Repository<AuctionRDbContext, Bid>, IBidRepository
{
    public BidRepository(AuctionRDbContext context) 
        : base(context)
    {
    }
}
