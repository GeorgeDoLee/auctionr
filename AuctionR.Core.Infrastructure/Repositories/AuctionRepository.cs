using AuctionR.Core.Domain.Entities;
using AuctionR.Core.Domain.Interfaces;
using AuctionR.Core.Infrastructure.Persistance;
using AuctionR.Shared.Repositories;

namespace AuctionR.Core.Infrastructure.Repositories;

internal class AuctionRepository : Repository<AuctionRDbContext, Auction>, IAuctionRepository
{
    public AuctionRepository(AuctionRDbContext context) 
        : base(context)
    {
    }
}
