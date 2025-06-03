namespace AuctionR.Core.Application.Contracts.Responses;

public class BidRetractedResponse
{
    public int? PreviousHighestBidId { get; set; }

    public int AuctionId { get; set; }

    public int? PreviousHighestBidderId { get; set; }

    public decimal PreviousHighestBidAmount { get; set; } = 0m;

    public DateTime? PreviousHighestBidTimestamp { get; set; }
}
