using AuctionR.Core.Domain.Entities;
using AuctionR.Core.Domain.Primitives;

namespace AuctionR.Core.Domain.Events;

public record BidPlacedEvent(Guid Id,  int AuctionId, int BidId, int BidderId, decimal Amount, DateTime Timestamp) 
    : DomainEvent(Id)
{
    public static BidPlacedEvent FromBid(Bid bid)
    {
        return new BidPlacedEvent(
                Id: Guid.NewGuid(),
                BidId: bid.Id,
                BidderId: bid.BidderId,
                AuctionId: bid.AuctionId,
                Amount: bid.Amount,
                Timestamp: bid.Timestamp);
    }
}
