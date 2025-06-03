using AuctionR.Core.Application.Contracts.Models;
using AuctionR.Core.Domain.Interfaces;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuctionR.Core.Application.Queries.Bids.GetByAuction;

public class GetBidsByAuctionQueryHandler : IRequestHandler<GetBidsByAuctionQuery, IEnumerable<BidModel>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetBidsByAuctionQueryHandler> _logger;

    public GetBidsByAuctionQueryHandler(
        IUnitOfWork unitOfWork,
        ILogger<GetBidsByAuctionQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<IEnumerable<BidModel>> Handle(GetBidsByAuctionQuery query, CancellationToken ct)
    {
        _logger.LogInformation(
            "trying to fetch bids for auction with id: {auctionId}, on page {pageNumber} with size {pageSize}.",
            query.AuctionId, query.PageNumber, query.PageSize);

        var bids = await _unitOfWork.Bids
            .GetByAuctionIdAsync(query.AuctionId, query.PageNumber, query.PageSize, ct);

        _logger.LogInformation(
            "Bids for auction with id: {aucdionId}, on page {pageNumber} with size {pageSize} fetched successfully.",
            query.AuctionId, query.PageNumber, query.PageSize);

        return bids.Adapt<List<BidModel>>();
    }
}
