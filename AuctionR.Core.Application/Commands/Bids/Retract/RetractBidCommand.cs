using AuctionR.Core.Application.Contracts.Models;
using MediatR;

namespace AuctionR.Core.Application.Commands.Bids.Retract;

public class RetractBidCommand : IRequest<AuctionModel?>
{
    public RetractBidCommand(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}
