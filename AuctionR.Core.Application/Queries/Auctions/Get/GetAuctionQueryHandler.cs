using AuctionR.Core.Application.Models;
using AuctionR.Core.Domain.Interfaces;
using Mapster;
using MediatR;

namespace AuctionR.Core.Application.Queries.Auctions.Get;

public class GetAuctionQueryHandler : IRequestHandler<GetAuctionQuery, AuctionModel?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAuctionQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<AuctionModel?> Handle(GetAuctionQuery query, CancellationToken ct)
    {
        var auction = await _unitOfWork.Auctions.GetAsync(query.Id, ct);

        if (auction == null)
        {
            return null;
        }

        return auction.Adapt<AuctionModel>();
    }
}
