using MediatR;

namespace AuctionR.Core.Application.Features.Auctions.Commands.Cancel;

public sealed class CancelAuctionCommand : IRequest<bool>
{
    public CancelAuctionCommand(int auctionId, int userId)
    {
        AuctionId = auctionId;
        UserId = userId;
    }

    public int AuctionId { get; set; }

    public int UserId { get; set; }
}
