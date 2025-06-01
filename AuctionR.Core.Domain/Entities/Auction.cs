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

    public IEnumerable<Bid>? Bids { get; set; }
}
