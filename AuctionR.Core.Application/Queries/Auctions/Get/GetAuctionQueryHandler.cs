using AuctionR.Core.Application.Models;
using AuctionR.Core.Domain.Exceptions;
using AuctionR.Core.Domain.Interfaces;
using Mapster;
using MediatR;

namespace AuctionR.Core.Application.Queries.Auctions.Get;

public class GetAuctionQueryHandler : IRequestHandler<GetAuctionQuery, AuctionModel>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAuctionQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<AuctionModel> Handle(GetAuctionQuery query, CancellationToken ct)
    {
        var auction = await _unitOfWork.Auctions.GetAsync(query.Id, ct);

        if (auction == null)
        {
            throw new NotFoundException($"auction with id: {query.Id} not found");
        }

        return auction.Adapt<AuctionModel>();
    }
}
