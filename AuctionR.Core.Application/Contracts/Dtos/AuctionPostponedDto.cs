namespace AuctionR.Core.Application.Contracts.Dtos;

public class AuctionPostponedDto
{
    public int AuctionId { get; set; }

    public DateTime NewStartTime { get; set; }

    public DateTime NewEndTime { get; set; }
}
