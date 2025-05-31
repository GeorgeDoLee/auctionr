using AuctionR.Core.Application.Models;
using AuctionR.Core.Domain.Interfaces;
using Mapster;
using MediatR;

namespace AuctionR.Core.Application.Queries.Auctions.GetAll;

public class GetAllAuctionsQueryHandler :
    IRequestHandler<GetAllAuctionsQuery, IEnumerable<AuctionModel>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllAuctionsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<AuctionModel>> Handle(
        GetAllAuctionsQuery request, CancellationToken ct)
    {
        var auctions = await _unitOfWork.Auctions.GetAllAsync();

        return auctions.Adapt<List<AuctionModel>>();
    }
}
