using AuctionR.Core.Application.Contracts.Models;
using AuctionR.Core.Domain.Exceptions;
using AuctionR.Core.Domain.Interfaces;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuctionR.Core.Application.Queries.Auctions.Get;

public class GetAuctionQueryHandler : IRequestHandler<GetAuctionQuery, AuctionModel>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<GetAuctionQueryHandler> _logger;
    public GetAuctionQueryHandler(
        IUnitOfWork unitOfWork,
        ILogger<GetAuctionQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<AuctionModel> Handle(GetAuctionQuery query, CancellationToken ct)
    {
        _logger.LogInformation("Trying to fetch auction with id: {id}", query.Id);
        var auction = await _unitOfWork.Auctions.GetAsync(query.Id, ct);

        if (auction == null)
        {
            _logger.LogWarning("Auction with id: {id} could not be found", query.Id);
            throw new NotFoundException($"auction with id: {query.Id} not found");
        }

        _logger.LogInformation("Auction with id: {id} fetched successfully", query.Id);
        return auction.Adapt<AuctionModel>();
    }
}
