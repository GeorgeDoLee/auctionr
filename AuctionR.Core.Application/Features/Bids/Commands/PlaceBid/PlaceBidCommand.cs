using AuctionR.Core.Application.Contracts.Models;
using MediatR;

namespace AuctionR.Core.Application.Features.Bids.Commands.PlaceBid;

public sealed class PlaceBidCommand : IRequest<BidModel>
{
    public int AuctionId { get; set; }

    public int BidderId { get; set; }

    public decimal Amount { get; set; }
}
