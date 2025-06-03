using AuctionR.Core.Application.Contracts.Models;
using AuctionR.Core.Domain.Exceptions;
using AuctionR.Core.Domain.Interfaces;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuctionR.Core.Application.Queries.Bids.Get;

public class GetBidQueryHandler : IRequestHandler<GetBidQuery, BidModel>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetBidQueryHandler> _logger;
    public GetBidQueryHandler(
        IUnitOfWork unitOfWork,
        ILogger<GetBidQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<BidModel> Handle(GetBidQuery query, CancellationToken ct)
    {
        _logger.LogInformation("Trying to get bid by id: {id}", query.Id);
        var bid = await _unitOfWork.Bids.GetAsync(query.Id, ct);

        if (bid == null)
        {
            _logger.LogWarning("Bid with id: {bidId} could not be found.", query.Id);
            throw new NotFoundException($"Bid with id: {query.Id} could not be found,");
        }

        _logger.LogInformation("Bid by id: {id} fetched successfully.", query.Id);
        return bid.Adapt<BidModel>();
    }
}
