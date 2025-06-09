using AuctionR.Core.Domain.Enums;
using AuctionR.Core.Domain.Events;
using AuctionR.Core.Domain.Primitives;

namespace AuctionR.Core.Domain.Entities;

public class Auction : Entity
{
    public int ProductId { get; set; }
    public int OwnerId { get; set; }

    public string? Title { get; set; }
    public string? Description { get; set; }

    public decimal StartingPrice { get; set; }
    public decimal MinimumBidIncrement { get; set; }
    public required string Currency { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public decimal? HighestBidAmount { get; set; }
    public int? HighestBidderId { get; set; }

    public AuctionStatus Status { get; set; }

    public ICollection<Bid> Bids { get; set; } = new List<Bid>();


    public void Postpone(DateTime startTime, DateTime endTime)
    {
        if (Status != AuctionStatus.Pending)
        {
            throw new InvalidOperationException("Only pending auctions can be postponed.");
        }

        StartTime = startTime;
        EndTime = endTime;

        Raise(new AuctionPostponedEvent(Guid.NewGuid(), Id, startTime, endTime));
    }

    public void Start()
    {
        if (Status != AuctionStatus.Pending)
        {
            throw new InvalidOperationException("Only pending auctions can be started manually.");
        }

        StartTime = DateTime.UtcNow;
        Status = AuctionStatus.Active;

        Raise(new AuctionStartedEvent(Guid.NewGuid(), Id, StartTime));
    }

    public void End()
    {
        if (Status != AuctionStatus.Active)
        {
            throw new InvalidOperationException("Only active auctions can be ended manually.");
        }

        EndTime = DateTime.UtcNow;
        Status = AuctionStatus.Ended;

        Raise(new AuctionEndedEvent(Guid.NewGuid(), Id, EndTime));
    }

    public void Cancel()
    {
        if (Status == AuctionStatus.Ended)
        {
            throw new InvalidOperationException("Cannot cancel an auction that has already ended.");
        }

        Status = AuctionStatus.Cancelled;
    }

    public void PlaceBid(Bid bid)
    {
        if (Status != AuctionStatus.Active)
        {
            throw new InvalidOperationException("Auction is not currently active.");
        }

        var now = DateTime.UtcNow;
        if (now < StartTime || now > EndTime)
        {
            throw new InvalidOperationException("Auction is not running at this time.");
        }

        if (HighestBidderId.HasValue && HighestBidderId == bid.BidderId)
        {
            throw new InvalidOperationException("You are already the highest bidder.");
        }

        var minAcceptableBid = HighestBidAmount.HasValue
            ? HighestBidAmount.Value + MinimumBidIncrement
            : StartingPrice;

        if (bid.Amount < minAcceptableBid)
        {
            throw new InvalidOperationException($"Bid amount must be at least {minAcceptableBid}.");
        }

        HighestBidAmount = bid.Amount;
        HighestBidderId = bid.BidderId;
        Bids.Add(bid);

        Raise(BidPlacedEvent.FromBid(bid));
    }
}