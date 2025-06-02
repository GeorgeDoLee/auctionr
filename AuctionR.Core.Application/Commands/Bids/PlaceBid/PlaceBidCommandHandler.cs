using AuctionR.Core.Application.Commands.Bids.Create;
using AuctionR.Core.Application.Contracts.Models;
using AuctionR.Core.Domain.Entities;
using AuctionR.Core.Domain.Enums;
using AuctionR.Core.Domain.Exceptions;
using AuctionR.Core.Domain.Interfaces;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuctionR.Core.Application.Commands.Bids.PlaceBid;

public class PlaceBidCommandHandler : IRequestHandler<PlaceBidCommand, AuctionModel?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<PlaceBidCommandHandler> _logger;
    public PlaceBidCommandHandler(
        IUnitOfWork unitOfWork,
        ILogger<PlaceBidCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<AuctionModel?> Handle(PlaceBidCommand command, CancellationToken ct)
    {
        _logger.LogInformation("Trying to place bid with properties: {@bid}", command);
        var auction = await _unitOfWork.Auctions.GetAsync(command.AuctionId, ct);

        if (auction == null)
        {
            _logger.LogWarning("Auction with id: {auctionId} could not be found.", command.AuctionId);
            throw new NotFoundException($"Auction with id: {command.AuctionId} could not be found.");
        }

        if (auction.Status != AuctionStatus.Active ||
            DateTime.UtcNow < auction.StartTime ||
            DateTime.UtcNow > auction.EndTime)
        {
            _logger.LogWarning("Bid cant be placed, because auction is not currently running.");
            throw new InvalidOperationException("Auction is not currently active.");
        }

        var minAcceptableBid = auction.HighestBidderId == null
            ? auction.StartingPrice
            : auction.HighestBidAmount + auction.MinimumBidIncrement;

        if (command.Amount < minAcceptableBid)
        {
            _logger.LogWarning("Bid cant be placed, becacuse bid mount is too low.");
            throw new InvalidOperationException("Bid amount is too low.");
        }

        var newBid = command.Adapt<Bid>();
        auction.HighestBidAmount = newBid.Amount;
        auction.HighestBidderId = newBid.BidderId;

        await _unitOfWork.Bids.AddAsync(newBid, ct);
        await _unitOfWork.Complete(ct);

        _logger.LogInformation("Bid placed successfully.");
        return auction.Adapt<AuctionModel>();
    }
}
