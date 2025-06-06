using AuctionR.Core.Application.Contracts.Models;
using AuctionR.Core.Domain.Interfaces;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuctionR.Core.Application.Features.Bids.Queries.Search;

internal sealed class SearchBidsQueryHandler : IRequestHandler<SearchBidsQuery, IEnumerable<BidModel>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<SearchBidsQueryHandler> _logger;

    public SearchBidsQueryHandler(
        IUnitOfWork unitOfWork,
        ILogger<SearchBidsQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<IEnumerable<BidModel>> Handle(SearchBidsQuery query, CancellationToken ct)
    {
        _logger.LogInformation("Searching bids with query: {@query}", query);

        var bidsQuery = _unitOfWork.Bids.Query(a =>
           (!query.AuctionId.HasValue || a.AuctionId == query.AuctionId) &&
           (!query.BidderId.HasValue || a.BidderId == query.BidderId) &&
           (!query.MaxAmount.HasValue || a.Amount <= query.MaxAmount) &&
           (!query.MinAmount.HasValue || a.Amount >= query.MinAmount)
        );

        var pagedBids = await _unitOfWork.Bids.GetPagedAsync(
            query.PageNumber,
            query.PageSize,
            bidsQuery,
            ct
        );

        _logger.LogInformation("Bids fetched successfully.");
        return pagedBids.Adapt<List<BidModel>>();
    }
}
