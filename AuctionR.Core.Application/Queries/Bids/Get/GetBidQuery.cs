using AuctionR.Core.Application.Contracts.Models;
using MediatR;

namespace AuctionR.Core.Application.Queries.Bids.Get;

public class GetBidQuery : IRequest<BidModel>
{
    public GetBidQuery(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}
