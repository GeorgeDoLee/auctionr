using AuctionR.Core.Application.Contracts.Models;
using AuctionR.Core.Domain.Interfaces;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuctionR.Core.Application.Queries.Auctions.Search;

public class SearchAuctionsQueryHandler : IRequestHandler<SearchAuctionsQuery, IEnumerable<AuctionModel>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<SearchAuctionsQueryHandler> _logger;

    public SearchAuctionsQueryHandler(
        IUnitOfWork unitOfWork,
        ILogger<SearchAuctionsQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<IEnumerable<AuctionModel>> Handle(SearchAuctionsQuery query, CancellationToken ct)
    {
        _logger.LogInformation("Searching auctions with query: {@query}", query);

        var auctionsQuery = _unitOfWork.Auctions.Query(a =>
           (!query.ProductId.HasValue || a.ProductId == query.ProductId) &&
           (!query.OwnerId.HasValue || a.OwnerId == query.OwnerId) &&
           (string.IsNullOrEmpty(query.Title) || a.Title!.Contains(query.Title)) &&
           (string.IsNullOrEmpty(query.Description) || a.Description!.Contains(query.Description)) &&
           (!query.MaxStartingPrice.HasValue || a.StartingPrice <= query.MaxStartingPrice) &&
           (string.IsNullOrEmpty(query.Currency) || a.Currency == query.Currency) &&
           (!query.MinStartTime.HasValue || a.StartTime >= query.MinStartTime) &&
           (!query.MaxEndTime.HasValue || a.EndTime <= query.MaxEndTime) &&
           (!query.MaxCurrentBidAmount.HasValue || a.HighestBidAmount <= query.MaxCurrentBidAmount) &&
           (!query.Status.HasValue || a.Status == query.Status)
       );

        var pagedAuctions = await _unitOfWork.Auctions.GetPagedAsync(
            query.PageNumber,
            query.PageSize,
            auctionsQuery,
            ct
        );

        _logger.LogInformation("Auctions fetched successfully.");
        return pagedAuctions.Adapt<List<AuctionModel>>();
    }
}