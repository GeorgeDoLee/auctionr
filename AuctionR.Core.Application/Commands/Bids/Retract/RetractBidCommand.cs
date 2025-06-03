using AuctionR.Core.Application.Contracts.Models;
using AuctionR.Core.Application.Contracts.Responses;
using MediatR;

namespace AuctionR.Core.Application.Commands.Bids.Retract;

public class RetractBidCommand : IRequest<BidRetractedResponse>
{
    public RetractBidCommand(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}
