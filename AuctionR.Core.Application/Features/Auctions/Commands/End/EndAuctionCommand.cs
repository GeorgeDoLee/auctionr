using MediatR;

namespace AuctionR.Core.Application.Features.Auctions.Commands.End;

public sealed class EndAuctionCommand : IRequest<bool>
{
    public EndAuctionCommand(int auctionId, int userId)
    {
        AuctionId = auctionId;
        UserId = userId;
    }

    public int AuctionId { get; set; }

    public int UserId { get; set; }
}
