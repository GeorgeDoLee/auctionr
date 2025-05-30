namespace AuctionR.Core.Domain.Entities;

public class Bid
{
    public int Id { get; set; }

    public int AuctionId { get; set; }

    public int BidderId { get; set; }

    public decimal Amount { get; set; }

    public DateTime Timestamp { get; set; }
}
