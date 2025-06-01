using AuctionR.Core.Application.Contracts.Models;
using AuctionR.Core.Domain.Interfaces;
using Mapster;
using MediatR;

namespace AuctionR.Core.Application.Queries.Bids.Get;

public class GetBidQueryHandler : IRequestHandler<GetBidQuery, BidModel>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetBidQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<BidModel> Handle(GetBidQuery query, CancellationToken ct)
    {
        var bids = await _unitOfWork.Bids.GetAsync(query.Id, ct);

        return bids.Adapt<BidModel>();
    }
}
