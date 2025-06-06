using AuctionR.Core.Application.Common.Guards;
using AuctionR.Core.Application.Contracts.Models;
using AuctionR.Core.Domain.Entities;
using AuctionR.Core.Domain.Interfaces;
using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuctionR.Core.Application.Features.Bids.Commands.PlaceBid;

internal sealed class PlaceBidCommandHandler : IRequestHandler<PlaceBidCommand, BidModel>
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

    public async Task<BidModel> Handle(PlaceBidCommand command, CancellationToken ct)
    {
        _logger.LogInformation("Trying to place bid with properties: {@bid}", command);
        var auction = await _unitOfWork.Auctions.GetAsync(command.AuctionId, ct);

        Guard.EnsureFound(auction, nameof(auction), command.AuctionId, _logger);

        var newBid = command.Adapt<Bid>();

        auction!.PlaceBid(newBid);
        await _unitOfWork.Bids.AddAsync(newBid, ct);
        await _unitOfWork.Complete(ct);

        _logger.LogInformation("Bid placed successfully.");
        return newBid.Adapt<BidModel>();
    }
}
