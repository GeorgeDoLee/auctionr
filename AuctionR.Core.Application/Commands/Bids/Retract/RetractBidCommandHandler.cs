using AuctionR.Core.Application.Contracts.Responses;
using AuctionR.Core.Domain.Exceptions;
using AuctionR.Core.Domain.Interfaces;
using AuctionR.Core.Infrastructure.Settings;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AuctionR.Core.Application.Commands.Bids.Retract;

public class RetractBidCommandHandler : IRequestHandler<RetractBidCommand, BidRetractedResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RetractBidCommandHandler> _logger;
    private readonly BidSettings _bidSettings;

    public RetractBidCommandHandler(
        IUnitOfWork unitOfWork,
        ILogger<RetractBidCommandHandler> logger,
        IOptions<BidSettings> bidSettingsOptions)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _bidSettings = bidSettingsOptions.Value;
    }

    public async Task<BidRetractedResponse> Handle(RetractBidCommand command, CancellationToken ct)
    {
        _logger.LogInformation("trying to retract bid with Id {bidId}", command.BidId);
        var bid = await _unitOfWork.Bids.GetAsync(command.BidId, ct);

        if (bid == null)
        {
            _logger.LogWarning("Bid with Id: {bidId} could not be found.", command.BidId);
            throw new NotFoundException($"Bid with id: {command.BidId} could not be found.");
        }

        if (bid.BidderId != command.BidderId)
        {
            _logger.LogWarning(
                "bidder id: {bidderId} did not match with incoming request user id: {userId}.",
                bid.BidderId, command.BidderId);
            throw new InvalidOperationException($"Bid id doesnt match incoming request user id.");
        }

        if (!bid.IsRetractable(_bidSettings.RetractableSeconds))
        {
            _logger.LogWarning("bid with Id: {bidId} could not be retracted.", bid.Id);
            throw new InvalidOperationException($"Bids may only be retracted within {_bidSettings.RetractableSeconds} seconds after being placed.");
        }

        var auction = await _unitOfWork.Auctions.GetWithBidsAsync(bid.AuctionId, ct);

        if (auction == null)
        {
            throw new NotFoundException($"Auction associated with bid Id: {bid.Id} could not be found.");
        }

        var previousHighestBid = auction.RetractBid(bid);

        _unitOfWork.Bids.Remove(bid);
        await _unitOfWork.Complete(ct);

        _logger.LogInformation("Bid retraction completed successfully for bid with Id {bidId}", bid.Id);

        return previousHighestBid == null
            ? new BidRetractedResponse { AuctionId = auction.Id }
            : previousHighestBid.Adapt<BidRetractedResponse>();
    }
}