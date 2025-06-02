using AuctionR.Core.Application.Contracts.Models;
using AuctionR.Core.Domain.Exceptions;
using AuctionR.Core.Domain.Interfaces;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuctionR.Core.Application.Commands.Bids.Retract;

public class RetractBidCommandHandler : IRequestHandler<RetractBidCommand, AuctionModel?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RetractBidCommandHandler> _logger;
    public RetractBidCommandHandler(
        IUnitOfWork unitOfWork,
        ILogger<RetractBidCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<AuctionModel?> Handle(RetractBidCommand command, CancellationToken ct)
    {
        _logger.LogInformation("trying to retract bid with Id {bidId}", command.Id);
        var bid = await _unitOfWork.Bids.GetAsync(command.Id, ct);

        if (bid == null)
        {
            _logger.LogWarning("Bid with Id: {bidId} could not be found.", command.Id);
            throw new NotFoundException($"Bid with id: {command.Id} could not be found.");
        }

        if (DateTime.UtcNow > bid.Timestamp.AddSeconds(30))
        {
            _logger.LogWarning("Auction for bid with Id: {bidId} not found.", bid.Id);
            throw new InvalidOperationException("Bids may only be retracted within 30 seconds after being placed.");
        }

        var auction = await _unitOfWork.Auctions.GetAsync(bid.AuctionId, ct);

        if (auction == null)
        {
            throw new NotFoundException($"Auction associated with bid Id: {bid.Id} could not be found.");
        }

        _logger.LogInformation("Retracting bid with Id {bidId} from Auction with Id {auctionId}", bid.Id, auction.Id);
        
        var previousHighestBid = auction.Bids
            .Where(b => b.Id != bid.Id)
            .MaxBy(b => b.Amount);

        if (previousHighestBid == null)
        {
            _logger.LogInformation("No previous bids found. Resetting highest bid info for Auction with Id {auctionId}", auction.Id);

            auction.HighestBidAmount = 0m;
            auction.HighestBidderId = null;
        }
        else
        {
            _logger.LogInformation("New highest bid after retraction: bid with Id {bidId} with amount {amount}",
                previousHighestBid.Id, previousHighestBid.Amount);
            
            auction.HighestBidAmount = previousHighestBid.Amount;
            auction.HighestBidderId = previousHighestBid.BidderId;
        }

        _unitOfWork.Bids.Remove(bid);
        await _unitOfWork.Complete(ct);

        _logger.LogInformation("Bid retraction completed successfully for bid with Id {bidId}", bid.Id);
        return auction.Adapt<AuctionModel>();
    }
}
