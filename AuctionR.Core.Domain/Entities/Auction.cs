using AuctionR.Core.Domain.Enums;

namespace AuctionR.Core.Domain.Entities;

public class Auction
{
    public int Id { get; set; }

    public int ProductId { get; set; }
    
    public decimal StartingPrice { get; set; }

    public decimal MinimumBidIncrement { get; set; }

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
    }

    public void Start()
    {
        if (Status != AuctionStatus.Pending)
        {
            throw new InvalidOperationException("Only pending auctions can be started manually.");
        }

        StartTime = DateTime.UtcNow;
        Status = AuctionStatus.Active;
    }

    public void End()
    {
        if (Status != AuctionStatus.Active)
        {
            throw new InvalidOperationException("Only active auctions can be ended manually.");
        }

        EndTime = DateTime.UtcNow;
        Status = AuctionStatus.Ended;
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
    }

    public Bid? RetractBid(Bid bid)
    {
        if (!Bids.Contains(bid))
        {
            throw new InvalidOperationException("Bid does not belong to this auction.");
        }

        Bids.Remove(bid);

        if (!Bids.Any())
        {
            HighestBidAmount = 0m;
            HighestBidderId = null;

            return null;
        }

        var previousHighestBid = Bids.MaxBy(b => b.Amount)!;
        HighestBidAmount = previousHighestBid.Amount;
        HighestBidderId = previousHighestBid.BidderId;

        return previousHighestBid;
    }
}