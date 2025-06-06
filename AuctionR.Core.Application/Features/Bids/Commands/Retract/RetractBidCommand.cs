using AuctionR.Core.Application.Contracts.Responses;
using MediatR;

namespace AuctionR.Core.Application.Features.Bids.Commands.Retract;

public sealed class RetractBidCommand : IRequest<BidRetractedResponse>
{
    public RetractBidCommand(int bidId, int userId)
    {
        BidId = bidId;
        UserId = userId;
    }

    public int BidId { get; set; }

    public int UserId { get; set; }
}
