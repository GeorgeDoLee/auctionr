using MediatR;

namespace AuctionR.Core.Application.Commands.Auctions.Postpone;

public class PostponeAuctionCommand : IRequest<bool>
{
    public int AuctionId { get; set; }

    public int UserId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }
}
