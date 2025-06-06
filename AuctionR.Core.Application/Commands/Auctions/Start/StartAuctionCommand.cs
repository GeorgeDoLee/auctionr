using MediatR;

namespace AuctionR.Core.Application.Commands.Auctions.Start;

public class StartAuctionCommand : IRequest<bool>
{
    public StartAuctionCommand(int auctionId, int userId)
    {
        AuctionId = auctionId;
        UserId = userId;
    }

    public int AuctionId { get; set; }

    public int UserId { get; set; }
}
