using MediatR;

namespace AuctionR.Core.Application.Commands.Auctions.End;

public class EndAuctionCommand : IRequest<bool>
{
    public EndAuctionCommand(int auctionId, int userId)
    {
        AuctionId = auctionId;
        UserId = userId;
    }

    public int AuctionId { get; set; }

    public int UserId { get; set; }
}
