using MediatR;

namespace AuctionR.Core.Application.Features.Auctions.Commands.Postpone;

public sealed class PostponeAuctionCommand : IRequest<bool>
{
    public int AuctionId { get; set; }

    public int UserId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }
}
