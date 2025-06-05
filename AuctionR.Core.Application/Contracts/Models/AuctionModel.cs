namespace AuctionR.Core.Application.Contracts.Models;

public class AuctionModel
{
    public int Id { get; set; }
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

    public decimal HighestBidAmount { get; set; }
    public int? HighestBidderId { get; set; }

    public string Status { get; set; } = string.Empty;

    public IEnumerable<BidModel>? Bids { get; set; }
}
