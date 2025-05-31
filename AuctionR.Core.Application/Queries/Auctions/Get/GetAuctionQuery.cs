using AuctionR.Core.Application.Models;
using MediatR;

namespace AuctionR.Core.Application.Queries.Auctions.Get;

public class GetAuctionQuery : IRequest<AuctionModel?>
{
    public GetAuctionQuery(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}
