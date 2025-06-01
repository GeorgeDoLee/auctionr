using AuctionR.Core.Application.Commands.Bids.Create;
using AuctionR.Core.Application.Contracts.Models;
using AuctionR.Core.Domain.Entities;
using AuctionR.Core.Domain.Enums;
using AuctionR.Core.Domain.Exceptions;
using AuctionR.Core.Domain.Interfaces;
using Mapster;
using MediatR;

namespace AuctionR.Core.Application.Commands.Bids.PlaceBid;

public class PlaceBidCommandHandler : IRequestHandler<PlaceBidCommand, AuctionModel?>
{
    private readonly IUnitOfWork _unitOfWork;

    public PlaceBidCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<AuctionModel?> Handle(PlaceBidCommand command, CancellationToken ct)
    {
        var auction = await _unitOfWork.Auctions.GetAsync(command.AuctionId, ct);

        if (auction == null)
        {
            throw new NotFoundException($"Auction with id: {command.AuctionId} could not be found.");
        }

        if (auction.Status != AuctionStatus.Active ||
            DateTime.UtcNow < auction.StartTime ||
            DateTime.UtcNow > auction.EndTime)
        {
            throw new InvalidOperationException("Auction is not currently active.");
        }

        if (auction.HighestBidderId != null && 
            command.Amount < auction.HighestBidAmount + auction.MinimumBidIncrement)
        {
            throw new InvalidOperationException("Bid amount is too low.");
        }

        var newBid = command.Adapt<Bid>();
        auction.HighestBidAmount = newBid.Amount;
        auction.HighestBidderId = newBid.BidderId;

        await _unitOfWork.Bids.AddAsync(newBid, ct);
        await _unitOfWork.Complete(ct);

        return auction.Adapt<AuctionModel>();
    }
}
