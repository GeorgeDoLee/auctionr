using AuctionR.Core.Application.Contracts.Models;
using MediatR;

namespace AuctionR.Core.Application.Features.Bids.Queries.Get;

public sealed class GetBidQuery : IRequest<BidModel>
{
    public GetBidQuery(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}
