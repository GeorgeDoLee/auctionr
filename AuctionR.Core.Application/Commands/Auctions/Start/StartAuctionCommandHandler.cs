using AuctionR.Core.Application.Common.Guards;
using AuctionR.Core.Domain.Entities;
using AuctionR.Core.Domain.Enums;
using AuctionR.Core.Domain.Exceptions;
using AuctionR.Core.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuctionR.Core.Application.Commands.Auctions.Start;

public class StartAuctionCommandHandler : IRequestHandler<StartAuctionCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<StartAuctionCommandHandler> _logger;

    public StartAuctionCommandHandler(
        IUnitOfWork unitOfWork,
        ILogger<StartAuctionCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<bool> Handle(StartAuctionCommand command, CancellationToken ct)
    {
        _logger.LogInformation("Tryin to manually start auction with id: {auctionId}", command.AuctionId);
        var auction = await _unitOfWork.Auctions.GetAsync(command.AuctionId, ct);

        Guard.EnsureFound(auction, nameof(auction), command.AuctionId, _logger);
        Guard.EnsureUserOwnsResource(auction!.OwnerId, command.UserId, nameof(auction), _logger);

        auction.Start();
        await _unitOfWork.Complete(ct);

        _logger.LogInformation("Auction with id: {auctionId} started successfully.", command.AuctionId);
        return true;
    }
}