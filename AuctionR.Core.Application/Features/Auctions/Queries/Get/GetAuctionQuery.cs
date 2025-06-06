using AuctionR.Core.Application.Contracts.Models;
using MediatR;

namespace AuctionR.Core.Application.Features.Auctions.Queries.Get;

public sealed class GetAuctionQuery : IRequest<AuctionModel>
{
    public GetAuctionQuery(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}
