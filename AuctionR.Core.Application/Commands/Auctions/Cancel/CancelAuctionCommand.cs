using MediatR;

namespace AuctionR.Core.Application.Commands.Auctions.Cancel;

public class CancelAuctionCommand : IRequest<bool>
{
    public CancelAuctionCommand(int auctionId, int userId)
    {
        AuctionId = auctionId;
        UserId = userId;
    }

    public int AuctionId { get; set; }

    public int UserId { get; set; }
}
