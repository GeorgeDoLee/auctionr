using AuctionR.Core.Application.Contracts.Models;
using AuctionR.Core.Application.Contracts.Responses;
using MediatR;

namespace AuctionR.Core.Application.Commands.Bids.Retract;

public class RetractBidCommand : IRequest<BidRetractedResponse>
{
    public RetractBidCommand(int bidId, int bidderId)
    {
        BidId = bidId;
        BidderId = bidderId;
    }

    public int BidId { get; set; }

    public int BidderId { get; set; }
}
