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

        var newBid = command.Adapt<Bid>();

        try
        {
            auction.PlaceBid(newBid);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning("Bid could not be placed: {Message}", ex.Message);
            throw;
        }

        await _unitOfWork.Bids.AddAsync(newBid, ct);
        await _unitOfWork.Complete(ct);

        _logger.LogInformation("Bid placed successfully.");
        return auction.Adapt<AuctionModel>();
    }
}
